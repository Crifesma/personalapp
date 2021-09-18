using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IValueEncript
    {
        string Encrypt(string encryptValue);

        string Desencrypt(string encryptValue);

        string Sha(string valueToSHA);
    }
}
