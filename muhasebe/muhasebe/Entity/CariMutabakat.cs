namespace Entity
{
    using System;
    using System.Runtime.CompilerServices;

    public class CariMutabakat : Cari
    {
        public int durum { get; set; }

        public string gonderen { get; set; }

        public string yanitlanmaTarihi { get; set; }

        public string yanitlayanMail { get; set; }

        public string gonderilmeTarihi { get; set; }
    }
}

