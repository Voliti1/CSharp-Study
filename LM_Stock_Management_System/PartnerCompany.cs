using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LM_Stock_Management_System
{
    internal class PartnerCompany
    {
        public string AccountNumber { get; set; }
        public string Company_id { get; set; } //사업자번호
        public string CompanyName { get; set; }
        public string Company_president { get; set; }
        public string PhoneNumber { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }

        public void ProduceProduct(int count)
        {
            Console.WriteLine($"{ProductName}이/가 {count}개 생산되었습니다.");
        }

        public void Order(int count)
        {
            Console.WriteLine($"{ProductName}을/를 {count}개를 발주하였습니다.");
        }

        public void GiveMoney(int money)
        {
            Console.WriteLine($"{CompanyName}의 {AccountNumber} 계좌로 {money}원을 입금하였습니다.");
        }

        public void DeliverProduct(int count)
        {
            Console.WriteLine($"{ProductName}을/를 {count}개 납품중입니다.");
        }
    }
}
