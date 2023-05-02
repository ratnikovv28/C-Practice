using System;
using System.ComponentModel;

namespace Practice_5
{
    public enum eType { Гуманитарный, Технический, Медицинский, Творческий };

    [Serializable]
    public class University
    {
        [DisplayName("Город"), Category("Сводка")]
        public string CityName { get; set; }

        [DisplayName("Название университета"), Category("Сводка")]
        public string UniversityName { get; set; }

        [DisplayName("Год основания"), Category("Сводка")]
        public int EstablishedYear { get; set; }

        [DisplayName("Логотип"), Category("Сводка")]
        public string Logo { get; set; }

        [DisplayName("Количество обучающихся"), Category("Сводка")]
        public int NumberOfStudents { get; set; }

        [DisplayName("Тип университета"), Category("Сводка")]
        public eType TypeOfUniversity { get; set; }

        [Browsable(false)]
        public string StrType
        {
            get => TypeOfUniversity.ToString();
            set => TypeOfUniversity.ToString();
        }

        public University(string cityName, string universityName, int establishedYear, string logo, int numberOfStudents, eType typeOfUniversity)
        {
            CityName = cityName;
            UniversityName = universityName;
            EstablishedYear = establishedYear;
            Logo = logo;
            NumberOfStudents = numberOfStudents;
            TypeOfUniversity = typeOfUniversity;
        }
        public University()
        {
            CityName = "Москва";
            UniversityName = "Университет";
            EstablishedYear = 0;
            Logo = "../../Images/default.png";
            NumberOfStudents = 0;
            TypeOfUniversity = eType.Технический;
        }
    }
}
