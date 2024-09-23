namespace SearchMaster.Core
{
    public class Validator
    {
        public bool IsValid => _isValid;
        public string Error => _error;

        private bool _isValid = true;
        private string _error = string.Empty;

        public Validator IsNotNull<T>(T value, string fieldName)
        {
            return Validate(
                value != null,
                $"{fieldName} cannot be null\n");
        }

        public Validator IsNotEmpty(string value, string fieldName)
        {
            return Validate(
                value != null && value != string.Empty,
                $"{fieldName} cannot be empty\n");
        }

        public Validator IsPositive(long value, string fieldName)
        {
            return IsPositive(Convert.ToDouble(value), fieldName);
        }

        public Validator IsPositive(decimal value, string fieldName)
        {
            return IsPositive(Convert.ToDouble(value), fieldName);
        }

        public Validator IsPositive(double value, string fieldName)
        {
            return Validate(
                value >= 0,
                $"{fieldName} cannot be lees than 0\n");
        }

        public Validator IsEarlyCurrentDate(DateTime value, string fieldName)
        {
            return Validate(
                value < DateTime.Now,
                $"{fieldName} cannot be later than the current date\n");
        }

        public Validator IsLaterCurrentDate(DateTime value, string fieldName)
        {
            return Validate(
                value >= DateTime.Now,
                $"{fieldName} cannot be early than the current date\n");
        }

        public Validator IsShorter(string value, int maxLength, string fieldName)
        {
            return Validate(
                value.Length <= maxLength,
                $"{fieldName} cannot be longer than {maxLength} characters\n");
        }

        public Validator IsLonger(string value, int minLength, string fieldName)
        {
            return Validate(
                value.Length >= minLength,
                $"{fieldName} cannot be shorter than {minLength} characters\n");
        }

        private Validator Validate(bool expression, string error)
        {
            if (!expression)
            {
                _isValid = false;
                _error += error;
            }

            return this;
        }
    }
}
