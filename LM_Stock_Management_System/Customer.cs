using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM_Stock_Management_System
{
    internal class Customer
    {
        public string Customer_id { get; set; }
        public string Customer_name { get; set; }
        public string Customer_resident_number { get; set; }
        public string Customer_gender { get; set; }
        public string Customer_address { get; set; }
        public string Customer_phone_number { get; set; }
        public string Customer_account { get; set; }
        public int Point { get; set; }

        public void BuyProduct(string product_id, string product_name, int count)
        {
            Console.WriteLine($"{Customer_id} {Customer_name}님이 {product_id} {product_name}을 {count}개 구매하였습니다.");
            Point += count;
            Console.WriteLine($"{Customer_id} {Customer_name}님의 포인트가 {count}점 적립되었습니다.");
        }

        public void ExchangeProduct(string product_id, string product_name, string product_id2, string product_name2)
        {
            Console.WriteLine($"{Customer_id} {Customer_name}님이 {product_id} {product_name}을 {product_id2} {product_name2}으로 교환하셨습니다.");
        }
        public void ReturnProduct(string product_id, string product_name, int count)
        {
            Console.WriteLine($"{Customer_id} {Customer_name}님이 {product_id} {product_name}을 {count}개를 반품하셨습니다.");
        }

        public void Refund(int money)
        {
            Console.WriteLine($"{Customer_id} {Customer_name}님께 {money}원을 환불합니다.");
        }
    }
}
