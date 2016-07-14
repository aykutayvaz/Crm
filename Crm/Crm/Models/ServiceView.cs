using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm.Models
{
    public class ServiceView
    {
        int servisID;

        public int ServisID
        {
            get { return servisID; }
            set { servisID = value; }
        }

        int projeID;

        public int ProjeID
        {
            get { return projeID; }
            set { projeID = value; }
        }

        int arızaID;

        public int ArızaID
        {
            get { return arızaID; }
            set { arızaID = value; }
        }

        string arızaDurumu;

        public string ArızaDurumu
        {
            get { return arızaDurumu; }
            set { arızaDurumu = value; }
        }

        DateTime kayıtTarihi;

        public DateTime KayıtTarihi
        {
            get { return kayıtTarihi; }
            set { kayıtTarihi = value; }
        }

        string servisNot;

        public string ServisNot
        {
            get { return servisNot; }
            set { servisNot = value; }
        }

        string bölgeİsmi;

        public string Bölgeİsmi
        {
            get { return bölgeİsmi; }
            set { bölgeİsmi = value; }
        }
        string projeİsmi;

        public string Projeİsmi
        {
            get { return projeİsmi; }
            set { projeİsmi = value; }
        }
    }
}