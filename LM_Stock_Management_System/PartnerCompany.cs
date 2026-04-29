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
        public string Company_id { get; set; } //사업자번호
        public string Company_name { get; set; }
        public string Company_president { get; set; }
        public string Company_phone_number { get; set; }
        public string Company_product_name { get; set; }
        public string Company_account { get; set; }
        public int Company_product_count { get; set; }

        public void ProduceProduct(int count)
        {
            Console.WriteLine($"{Company_product_name}가 {count}개 생산되었습니다.");
        }

        public void Order(int count)
        {
            Console.WriteLine($"{Company_product_name}을/를 {count}개를 발주하였습니다.");
        }

        public void GiveMoney(int money)
        {
            Console.WriteLine($"{Company_id} {Company_name} {Company_account}로 {money}원을 입금하였습니다.");
        }

        public void DeliverProduct(int count)
        {
            Console.WriteLine($"{Company_product_name}을/를 {count}개 납품중입니다.");
        }
    }
}
