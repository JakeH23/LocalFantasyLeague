using System.ComponentModel.DataAnnotations;

namespace LocalFantasyLeague.Models.DbSets
{
    public class Season : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Season name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "End date must be after the start date.")]
        public DateTime EndDate { get; set; }
    }

    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = value as DateTime?;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                return new ValidationResult($"Property '{_comparisonProperty}' not found.");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance) as DateTime?;

            if (currentValue.HasValue && comparisonValue.HasValue && currentValue <= comparisonValue)
            {
                return new ValidationResult(ErrorMessage ?? $"The date must be greater than {_comparisonProperty}.");
            }

            return ValidationResult.Success;
        }
    }
}
