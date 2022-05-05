using AutoMapper;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Services.Dtos.Spending;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public interface ISpendingService
    {
        Task<bool> AddSpendingAsync(AddSpendingDto spending, MoneyUser moneyUser);
        GetSpendingsDto GetSpendingsByCategoryAndUser(Guid categoryId, MoneyUser moneyUser);
        //GetSpendingsDto GetSpendingsByMonthAndUser(Guid categoryId, MoneyUser moneyUser);
        Task<bool> DeleteSpending(Guid spendingId);
        Task<AddSpendingDto> GetPictureInfo(byte[] picture, string fileName, MoneyUser user);
        string SavePicture(byte[] picture, string fileName);
    }
    public class SpendingService : ISpendingService { 
    
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public SpendingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddSpendingAsync(AddSpendingDto spending, MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var currentMonth = _unitOfWork.Months.GetCurrentMonth(moneyUser.Id);
            var moneySpent = currentMonth.Spendings.Sum(s => s.Cost);

            if (spending.Id != null)
            {
                var oldSpending = _unitOfWork.Spendings.GetById((Guid)spending.Id);
                if (oldSpending == null)
                    throw new BadRequestException(ErrorService.SpendingNotFound);
                if (oldSpending.Month.MonthOfYear != DateTime.UtcNow.Month || oldSpending.Month.Year != DateTime.UtcNow.Year)
                    throw new BadRequestException(ErrorService.SpendingExpired);
                
                if (moneySpent - oldSpending.Cost + spending.Cost > currentMonth.Budget)
                    throw new BadRequestException(ErrorService.NotEnoughMoney);
                oldSpending.CategoryId = spending.CategoryId;
                oldSpending.Cost = spending.Cost;
                oldSpending.Details = spending.Details;
                _unitOfWork.Spendings.Update(oldSpending);                 
            }
            else
            {
                var newSpending = _mapper.Map<Spending>(spending);
                if (moneySpent + spending.Cost > currentMonth.Budget)
                    throw new BadRequestException(ErrorService.NotEnoughMoney);
                newSpending.MonthId = currentMonth.Id;
                _unitOfWork.Spendings.Insert(newSpending);
            }

            return await _unitOfWork.SaveChangesAsync();
        }

        public GetSpendingsDto GetSpendingsByCategoryAndUser(Guid categoryId, MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var result = new GetSpendingsDto();
            var category = _unitOfWork.Categories.GetWithSpendings(categoryId);
            result.CategoryName = category.Name;
            result.CategoryId = categoryId;
            var spendingsDto = _mapper.Map<List<GetSpendingDto>>(category.Spendings);
            var orderedSpendings = spendingsDto.OrderByDescending(s => s.CreatedAt);
            result.Spendings = spendingsDto;
            return result;
        }

        public async Task<bool> DeleteSpending(Guid spendingId)
        { 
            var spending = _unitOfWork.Spendings.GetById(spendingId);
            if (spending == null)
                throw new BadRequestException(ErrorService.SpendingNotFound);
            if (spending.Month.MonthOfYear != DateTime.UtcNow.Month || spending.Month.Year != DateTime.UtcNow.Year)
                throw new BadRequestException(ErrorService.SpendingExpired);
            _unitOfWork.Spendings.Delete(spending);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<AddSpendingDto> GetPictureInfo(byte[] picture, string fileName, MoneyUser user)
        {
            var imgFile = SavePicture(picture, fileName);
            
            var process = System.Diagnostics.Process.Start(@"C:\Program Files\Tesseract-OCR\tesseract.exe",
                $@"{imgFile} C:\Users\CST00072\Mari\Facultate\output");
            process.WaitForExit();
            var output = System.IO.File.ReadAllText(@"C:\Users\CST00072\Mari\Facultate\output.txt");
            string data = GetBetween(output, "TOTAL", "T");
            decimal total;
            var totalString = Regex.Match(data, @"\d+.+\d").Value;
            totalString = totalString.Replace(",", ".");
            decimal.TryParse(totalString, out total);
            var category = _unitOfWork.Categories.GetByNameAndMoneyUser("Various", user.Id);
            if (category == null)
            { 
                category = new Category
                {
                    MoneyUserId = user.Id,
                    Name = "Various"
                };
            _unitOfWork.Categories.Insert(category);
            await _unitOfWork.SaveChangesAsync();
            }
            
            var result = new AddSpendingDto
            {
                CategoryId = category.Id,
                Cost = total,
                Details = (GetBetween(output, "RON", "CARD") != "" ) ? GetBetween(output, "RON", "CARD") : GetBetween(output, "RON", "CASH")
            };
            return result;
        } 

        public string SavePicture(byte[] picture, string fileName)
        {
            var path = Path.Combine(@"C:\Users\CST00072\Mari\Facultate\Licence\XamarinApp\Images", fileName);
            if (picture == null)
                throw new BadRequestException(ErrorService.InvalidValue);
            File.WriteAllBytes(path, picture);
            return path;
        }

        private static string GetBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }
        //public GetSpendingsDto GetSpendingsByMonthAndUser(Guid categoryId, MoneyUser moneyUser)
        //{
        //    if (moneyUser == null)
        //        throw new BadRequestException(ErrorService.NoUserFound);
        //    var result = new GetSpendingsDto();
        //    var month = _unitOfWork.Months.GetCurrentMonth(moneyUser.Id);
        //    var spendingsDto = _mapper.Map<List<GetSpendingDto>>(month.Spendings);
        //    var orderedSpendings = spendingsDto.OrderByDescending(s => s.CreatedAt);
        //    result.Spendings = spendingsDto;
        //    return result;
        //}


    }
}
