using SearchMaster.DataAccess.Repositories;
using System.Text.RegularExpressions;

namespace SearchMaster.Application.Services
{
    enum Language
    {
        Russian,
        English,
        Unknown
    }

    public class UsernameService(IWorkerRepository workerRepository) : IUsernameService
    {
        private readonly IWorkerRepository _workerRepository = workerRepository;

        private readonly Regex _ruLanguage = new(@"^[А-я]+$");
        private readonly Regex _enLanguage = new(@"^[A-z]+$");

        private readonly Dictionary<char, string> _characters = new()
        {
            { 'а', "A" },
            { 'б', "B" },
            { 'в', "V" },
            { 'г', "G" },
            { 'д', "D" },
            { 'е', "E" },
            { 'ё', "E" },
            { 'ж', "Zh" },
            { 'з', "Z" },
            { 'и', "I" },
            { 'й', "I" },
            { 'к', "K" },
            { 'л', "L" },
            { 'м', "M" },
            { 'н', "N" },
            { 'о', "O" },
            { 'п', "P" },
            { 'р', "R" },
            { 'с', "S" },
            { 'т', "T" },
            { 'у', "U" },
            { 'ф', "F" },
            { 'х', "Kh" },
            { 'ц', "Ts" },
            { 'ч', "Ch" },
            { 'ш', "Sh" },
            { 'щ', "Shch" },
            { 'ь', "" },
            { 'ы', "Y" },
            { 'ъ', "" },
            { 'э', "E" },
            { 'ю', "Yu" },
            { 'я', "Ya" },
        };

        public async Task<string> Generate(string surname, string name)
        {
            var language = DefineLanguage(surname, name);

            if (language == Language.Russian)
                return await RussianUsername(surname, name);
            else if (language == Language.English)
                return await EnglishUsername(surname, name);

            throw new ArgumentException("Invalid data format");
        }

        private async Task<string> AddNumber(string username)
        {
            var number = await _workerRepository.GetMaxUsernameNumber(username);

            if (number == null)
                return username;

            return username + (number + 1);
        }

        private Language DefineLanguage(string surname, string name)
        {
            if (_ruLanguage.IsMatch(surname) && _ruLanguage.IsMatch(name))
                return Language.Russian;
            else if (_enLanguage.IsMatch(surname) && _enLanguage.IsMatch(name))
                return Language.English;
            else
                return Language.Unknown;
        }

        private async Task<string> RussianUsername(string surname, string name)
        {
            surname = surname.ToLower();
            name = name.ToLower();

            string username = string.Empty;

            for (int i = 0; i < surname.Length; i++)
            {
                if (i == 0)
                    username += _characters[surname[i]];
                else
                    username += _characters[surname[i]].ToLower();
            }

            username += _characters[name[0]];

            return await AddNumber(username);
        }

        private async Task<string> EnglishUsername(string surname, string name)
        {
            string username = string.Empty;

            for (int i = 0; i < surname.Length; i++)
            {
                if (i == 0)
                    username += surname[i].ToString().ToUpper();
                else
                    username += surname[i].ToString().ToLower();
            }

            username += name[0].ToString().ToUpper();

            return await AddNumber(username);
        }
    }
}
