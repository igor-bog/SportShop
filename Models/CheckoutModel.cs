using System.ComponentModel.DataAnnotations;

namespace test.Models
{
    public class CheckoutModel
    {
        [Required(ErrorMessage = "Выберите тип карты")]
        public string CardType { get; set; }

        [Required(ErrorMessage = "Введите имя владельца")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Введите номер карты")]
        [StringLength(16, MinimumLength = 13, ErrorMessage = "Номер карты должен содержать от 13 до 16 цифр")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Введите срок действия")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "Введите CVV/CVC")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV должен содержать 3-4 цифры")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "Введите почтовый индекс")]
         [StringLength(10, MinimumLength = 2, ErrorMessage = "Неверный ввод")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Введите адрес доставки")]
        public string ShippingAddress { get; set; }
    }
}
