using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm.Models
{
    public class DefectView
    {
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

        string müdahaleEdenİsim;

        public string MüdahaleEdenİsim
        {
            get { return müdahaleEdenİsim; }
            set { müdahaleEdenİsim = value; }
        }

        string sikayetEden;

        public string SikayetEden
        {
            get { return sikayetEden; }
            set { sikayetEden = value; }
        }

        string sikayetEdenDetay;

        public string SikayetEdenDetay
        {
            get { return sikayetEdenDetay; }
            set { sikayetEdenDetay = value; }
        }

        Nullable<DateTime> kayıtTarihi;

        public Nullable<DateTime> KayıtTarihi
        {
            get { return kayıtTarihi; }
            set {
                  kayıtTarihi = value;

            }
        }

        string arızaNot;

        public string ArızaNot
        {
            get { return arızaNot; }
            set { arızaNot = value; }
        }

        string projeİsmi;

        public string Projeİsmi
        {
            get { return projeİsmi; }
            set { projeİsmi = value; }
        }

        int projeId;

        public int ProjeId
        {
            get { return projeId; }
            set { projeId = value; }
        }

        Nullable<int> müdahaleEdenId;

        public Nullable<int> MüdahaleEdenId
        {
            get { return müdahaleEdenId; }
            set { müdahaleEdenId = value; }
        }

        string müdahaleNotu;

        public string MüdahaleNotu
        {
            get { return müdahaleNotu; }
            set { müdahaleNotu = value; }
        }

        Nullable<DateTime> müdahaleTarihi;

        public Nullable<DateTime> MüdahaleTarihi
        {
            get { return müdahaleTarihi; }
            set { müdahaleTarihi = value; }
        }
    }
}