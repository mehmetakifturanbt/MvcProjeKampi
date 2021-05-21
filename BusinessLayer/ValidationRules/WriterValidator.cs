using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar Adını Boş Geçemezsiniz");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Yazar Soyadını Boş Geçemezsiniz");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında Kısmını Boş Geçemezsiniz");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Ünvan Kısmını Boş Geçemezsiniz");
            RuleFor(x => x.WriterAbout).Matches(@"[Aa]+").WithMessage("Hakkında Kısmında Mutlaka A Harfi Geçmeli (ÖDEV)");
            //RuleFor(x => x.WriterAbout).Must(isContains).WithMessage("Hakkında Kısmında Mutlaka A Harfi Geçmeli (ÖDEV)");
            RuleFor(x => x.WriterSurName).MinimumLength(3).WithMessage("Lütfen En Az 2 Karakter Girişi Yapınız");
            RuleFor(x => x.WriterSurName).MaximumLength(20).WithMessage("Lütfen 50 Karakterden Fazla Değer Girişi Yapmayınız");
        }

        //private bool isContains(string about)
        //{
        //    bool result = about.Contains("a");
        //    return result;

        //}
    }
}
