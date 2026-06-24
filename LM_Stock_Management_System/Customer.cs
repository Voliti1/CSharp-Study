using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM_Stock_Management_System
{
    internal class Customer : Person
    {
        public string CustomerID { get; set; }

        public int Points { get; set; }

        public void BuyProduct(string product_id, string product_name, int count)
        {
            Console.WriteLine($"{CustomerID} {Name}님이 {product_id} {product_name}을 {count}개 구매하였습니다.");
            Points += count;
            Console.WriteLine($"{CustomerID} {Name}님의 포인트가 {count}점 적립되었습니다.");
        }

        public void ExchangeProduct(string product_id, string product_name, string product_id2, string product_name2)
        {
            Console.WriteLine($"{CustomerID} {Name}님이 {product_id} {product_name}을 {product_id2} {product_name2}으로 교환하셨습니다.");
        }
        public void ReturnProduct(string product_id, string product_name, int count)
        {
            Console.WriteLine($"{CustomerID} {Name}님이 {product_id} {product_name}을 {count}개를 반품하셨습니다.");
        }

        public void Refund(int money)
        {
            Console.WriteLine($"{CustomerID} {Name}님께 {money}원을 환불합니다.");
        }
    }
}
