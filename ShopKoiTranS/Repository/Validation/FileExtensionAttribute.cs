﻿using System.ComponentModel.DataAnnotations;

namespace ShopKoiTranS.Repository.Validation
{
    public class FileExtensionAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file) {
                var extension =Path.GetExtension(file.FileName);
                string[] extensions =
                {

                    "jpg","png","jpeg"
                };
                bool result = extensions.Any(x => extension.EndsWith(x));
                if (!result) {
                    return new ValidationResult("Chỉ cho sử dụng ảnh định dạng jpg hoặc png hoặc jpeg"); }

            }
            return ValidationResult.Success;
        }
    }
}
