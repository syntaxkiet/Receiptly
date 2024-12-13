using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public static class ReceiptApiDtoHelper
    {
        public static ReceiptApiDto ConvertReceiptToDto(Receipt original)
        { 
            var result = new ReceiptApiDto()
            {
                Id = original.Id,
                PurchaseDate = original.PurchaseDate,
                StoreName = original.StoreName,
            };
            foreach (var item in original.Items)
            {
                var newItem = new ReceiptItemApiDto()
                {
                    Id = item.Id,
                    BestBeforeDate = item.BestBeforeDate,
                    Name = item.Name,
                    Quantity = item.Quantity,
                };
                result.Items.Add(newItem);
            }
            return result;
        }
    }
}
