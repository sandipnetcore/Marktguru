using MarktguruAssignment.DataModels.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktguruAssignment.DataModels.ModelExtension
{
    public static class HelpersExtendedMethods
    {
        public static List<ProductDataModel> ConvertToProductDataModelFromDataTable(this DataTable dt)
        {
            List<ProductDataModel> products = new List<ProductDataModel>();

            foreach(DataRow dr in dt.Rows)
            {
                ProductDataModel productDataModel = new ProductDataModel()
                {
                    ProductId = Convert.ToInt32(dr["ProductId"].ToString()),
                    ProductName = Convert.ToString(dr["ProductName"]),
                    Availability = Convert.ToBoolean(dr["ProductAvailability"].ToString()),
                    Price = Convert.ToDecimal(dr["ProductPrice"].ToString()),
                };

                products.Add(productDataModel);
            }

            return products;
        }

        public static ProductDetailDataModel ConvertToProductDetailsDataModelFromDataTable(this DataTable dt)
        {
            ProductDetailDataModel productDetailDataModel = new ProductDetailDataModel();

            foreach (DataRow dr in dt.Rows)
            {
                productDetailDataModel.ProductId = Convert.ToInt32(dr["ProductId"].ToString());
                productDetailDataModel.ProductName = Convert.ToString(dr["ProductName"]);
                productDetailDataModel.Availability = Convert.ToBoolean(dr["ProductAvailability"].ToString());
                productDetailDataModel.Price = Convert.ToDecimal(dr["ProductPrice"].ToString());
                productDetailDataModel.ProductDescription = Convert.ToString(dr["ProductDescription"]);
            }

            return productDetailDataModel;
        }
    }
}
