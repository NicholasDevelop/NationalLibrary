using System.Text.RegularExpressions;

namespace NationalLibrary.Data
{
	public static class DataController
	{
		/// <summary>
		/// Chek if the string 'city', 'name', 'surname'...
		/// </summary>
		/// <param name="str"></param>Input String
		/// <param name="sender"></param>Name/Surname/City...
		/// <returns>The input that is in the right formact</returns>
		/// <exception cref="Exception"></exception>
		public static string CheckStrings(string str, string sender)
		{
			string a = str ?? throw new ArgumentNullException($"Inserisci un/una {sender}");
			for (int i = 0; i < str.Length; i++)
				if (int.TryParse(str[i].ToString(), out int b))

					throw new Exception($"Non inserire numeri all'interno del/della {sender}");
			return str;
		}

		/// <summary>
		/// Check if Scaffhold or Shelf are null or if they contain a number
		/// </summary>
		/// <param name="c"></param>
		/// <param name="sender"></param>
		/// <returns>The Scaffhold or the Shelf in the right format/an exception</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exception"></exception>
		public static string CheckScaffholdOrShelf(string c, string sender)
		{
			string a = c ?? throw new ArgumentNullException($"Inserisci il/lo {sender}");
			if (int.TryParse(c.ToString(), out int b))

				throw new Exception($"Non inserire numeri nel/nello {sender}");
			return c;
		}

		/// <summary>
		/// Check if the Email is in the right format.
		/// </summary>
		/// <param name="email"></param>
		/// <returns>The email or an exception</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exception"></exception>
		public static string CheckEmail(string email)
		{
			string a = email ?? throw new ArgumentNullException("Inserisci l'email");
			if (!email.Contains("@"))
				throw new Exception("L'email non è scritta in un formato corretto!");
			return email;
		}

		/// <summary>
		/// Check if the document number is in the right format
		/// </summary>
		/// <param name="document"></param>
		/// <returns>The document number or an exception</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exception"></exception>
		public static string CheckDocumentNumber(string document)
		{
			string a = document ?? throw new ArgumentNullException("Inserisci il numero del documento!");
			if (document.Length != 9)
				throw new Exception("La carta d'identità è composta da soli 9 caratteri!");
			if (!int.TryParse(document[2].ToString() + document[3].ToString() + document[4].ToString() + document[5].ToString() + document[6].ToString(), out int b))
				throw new Exception("Carta d'identità non valida!");
			if (!char.IsLetter(document[0]) && !char.IsLetter(document[1]) && !char.IsLetter(document[7]) && !char.IsLetter(document[8]))
				throw new Exception("La carta d'identità deve iniziare con due lettere e finire con due lettere!");
			return document;
		}

		public static string CheckPassword(string password)
		{
			string a = password ?? throw new ArgumentNullException("Inserisci una password");
			Regex rgx = new Regex("^(?=.?[A-Z])(?=.?[a-z])(?=.?[0-9])(?=.?[#?!@$%^&-]).{8,}$");
			if (!rgx.IsMatch(password))
				throw new Exception("La password deve avere almeno un numero, un carattere maiuscolo e deve contenere almeno 8 caratteri")
			return password;
        }

	}
}
