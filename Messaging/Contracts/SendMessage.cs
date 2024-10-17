using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Contracts
{
  // SendMessage // Command tipinde bir ifade olması sebebi ile
  // Command => Direct Exchange Kullanırız. // Send ile gönderim yapacağız.
  // Queuee Send işleminde verileri tanımlanmış bir kuyruk yapısı üzerinden oluştururuz.
  public record SendMessage(string message);
 
}
