using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace AltaMontanha.Utilitario
{
	internal static class Seguranca
	{
		/// <summary>
		/// Criptografa um valor
		/// </summary>
		/// <param name="valor">Qualquer valor, geralmente uma Senha</param>
		/// <returns></returns>
		internal static string Criptografar(string valor)
		{
			MD5 md5Hasher = new MD5CryptoServiceProvider();
			byte[] dataByte = md5Hasher.ComputeHash(Encoding.ASCII.GetBytes(valor));

			return Convert.ToBase64String(dataByte);
		}
	}
}