namespace Entity
{
    using System;
    using System.Runtime.CompilerServices;

    public class Company
    {
        public int ID { get; set; }

        public string unvan { get; set; }

        public string vkno { get; set; }

        public int belgeSayisi { get; set; }

        public string malHizmetBedeli { get; set; }

        public string email { get; set; }

        public bool mailGonderim { get; set; }

        public int donemAy { get; set; }

        public int donemYil { get; set; }

        public int mutabakatTipi { get; set; }
    }
}

