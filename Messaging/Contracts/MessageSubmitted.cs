using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Contracts
{
  // Message ilgili Consumer'a iletildiğinde yapılacak olan işlemler için bu event kullanılır.
  // Event tanımı yaparken dili geçmiş zaman kipi kullanırız.
  // publish methodu ile gönderim sağlarız.
  public record MessageSubmitted(string message);
 
}
