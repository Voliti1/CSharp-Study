using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM_Stock_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee employee1 = new Employee
            {
                Employee_id = "001",
                Employee_name = "서준원",
                Employee_resident_number = "000101-3010101",
                Employee_gender = "남",
                Employee_address = "경기 화성시",
                Employee_phone_number = "010-0001-0001",
                Employee_rank = "사원",
                Employee_birth = "01 - 01",
                Employee_account = "110-001-001001",
                Employee_duty = "미정",
                Employee_status = "퇴근"
            };

            employee1.GoToWork();
            employee1.ManageStock();
            employee1.Cleaning();
            employee1.LeaveWork();

            Customer customer1 = new Customer {
                Customer_id = "A-000001",
                Customer_name = "홍길동",
                Customer_resident_number = "010101-3010102",
                Customer_gender = "남",
                Customer_address = "경기 수원시",
                Customer_phone_number = "010-0001-0002",
                Customer_account = "110-001-000002",
                Point = 0
            };
            
            customer1.BuyProduct("N-001", "신라면", 3);
            customer1.ExchangeProduct("N-001", "신라면", "O-001", "진라면");
            customer1.Refund(50000);

            PartnerCompany nongshim = new PartnerCompany
            {
                Company_id = "001-01-00001",
                Company_name = "농심",
                Company_president = "이병학",
                Company_phone_number = "031-0001-0001",
                Company_product_name = "신라면",
                Company_account = "000001-01-000001",
                Company_product_count = 500
            };

            PartnerCompany otoki = new PartnerCompany
            {
                Company_id = "002-02-00002",
                Company_name = "오뚜기",
                Company_president = "함영준",
                Company_phone_number = "031-0002-0002",
                Company_product_name = "진라면",
                Company_account = "000002-02-000002",
                Company_product_count = 500
            };

            nongshim.ProduceProduct(100);
            nongshim.GiveMoney(10000000);
            nongshim.ProduceProduct(200);

            Product product1 = new Product { 
                Product_id = "N-001",
                Product_name = "신라면",
                Count = 500,
                Price = 2000,
                ExpirationDate = "2026-04-29",
                Classification = "라면"
            };

            Product product2 = new Product
            {
                Product_id = "O-001",
                Product_name = "진라면",
                Count = 500,
                Price = 2000,
                ExpirationDate = "2026-04-29",
                Classification = "라면"
            };
        }
    }
}
