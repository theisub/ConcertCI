using ConcertCI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace ConcertCI
{
    public class ConcertActions
    {
        public DbSet<tblConcerts> SelectAllConcerts()
        {
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            return concertDB.tblConcerts;
        }
        public  void InsertConcert(Concert concert, int group)
        {
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            var conc = new tblConcerts();

            conc.concert_id = concert.ID;
            conc.group_id = group;
            conc.concert_city = concert.Location;
            conc.concert_date = concert.Date;
            conc.concert_link = concert.Link;
            conc.concert_place = concert.Place;
            conc.concert_title = concert.Title;

            concertDB.tblConcerts.Add(conc);
            concertDB.SaveChanges();
        }
        public void InsertConc(tblConcerts concert, int group)
        {
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            var conc = new tblConcerts();

            conc.concert_id = concert.concert_id;
            conc.group_id = group;
            conc.concert_city = concert.concert_city;
            conc.concert_date = concert.concert_date;
            conc.concert_link = concert.concert_link;
            conc.concert_place = concert.concert_place;
            conc.concert_title = concert.concert_title;

            concertDB.tblConcerts.Add(conc);
            concertDB.SaveChanges();
        }

        public void InsertConcerts(Concert concert, int group)
        {
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            var conc = new tblConcerts();

            conc.concert_id = group + 1;
            conc.group_id = group;
            conc.concert_city = "Test location";
            conc.concert_date = System.DateTime.Now;
            conc.concert_link = "no url";
            conc.concert_place = "place location";
            conc.concert_title = "Test ";

            concertDB.tblConcerts.Add(conc);
            concertDB.SaveChanges();
        }

        public tblConcerts FindGroupIdByConcert(int id)
        {
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            var result = concertDB.tblConcerts.Where(b => b.concert_id == id);

            if (result.Count() > 0)
                return result.First();
            else
                return null;


        }

        public string ShowConcList(int user)
        {
            UserActions userActions = new UserActions();
            SubscriptionActions subscriptionActions =new  SubscriptionActions();
         
            GroupActions groupActions = new GroupActions();

            string answer = null;
            if (userActions.ContainsUser(user))
            {
                var select = subscriptionActions.SelectSubscriptions(user);
                if (select != null)
                {
                    DbSet<tblConcerts> concerts = SelectAllConcerts();
                    
                    int i = 0;
                    foreach (var item in select)
                    {

                        var id = item.group_id;
                        var find = groupActions.FindGroupById(id);

                        if (find != null)
                        {

                        }
                       

                        for (i = 0; i < concerts.Count(); i++)
                        {
                            foreach (var conc in concerts)
                                answer += String.Format("���� � �������� ������ {0} ��������: {1} �����: {2} �����: " +
                                                        "{3} ������ �� ��������: {4} \n", conc.group_id, conc.concert_title, conc.concert_city, conc.concert_date, conc.concert_link);

                        }
                       
                    }

                    if (select.Count() == 0)
                    {
                        answer = "�� ������� �� ������ �������� � ����� ������ � ������������ �� ������ ������ �������� ";
                    }

                   

                }
               
            }

            return answer;

        }


        public string ShowConcList(int user, string user_city)
        {
            UserActions userActions = new UserActions();
            SubscriptionActions subscriptionActions = new SubscriptionActions();

            GroupActions groupActions = new GroupActions();
            bool atleastone = false;
            string answer = null;

            if (userActions.ContainsUser(user))
            {
                var select = subscriptionActions.SelectSubscriptions(user);
                if (select != null)
                {
                    DbSet<tblConcerts> concerts = SelectAllConcerts();

                    int i = 0;
                    foreach (var item in select)
                    {

                        var id = item.group_id;
                        var find = groupActions.FindGroupById(id);

                        if (find != null)
                        {

                        }
                        else
                            answer = "�� ������� �� ������ �������� � ����� ������ � ������������ �� ������ ������ �������� ";

                        for (i = 0; i < concerts.Count(); i++)
                        {
                            foreach (var conc in concerts)
                            {
                                if (conc.concert_city == user_city)
                                {
                                    answer += String.Format(
                                        "���� � �������� ������ {0} ��������: {1} �����: {2} �����: " +
                                        "{3} ������ �� ��������: {4} \n", conc.group_id, conc.concert_title,
                                        conc.concert_city, conc.concert_date, conc.concert_link);
                                    atleastone = true;
                                }
                                
                            }

                        }

                    }

                }
                else
                {
                    answer = "�������� ������ � ������� ������� \"/add <������>\".";
                }
            }

            if (atleastone == false)
                answer = "�� ������� �� ������ �������� � ����� ������ � ������������ �� ������ ������ ��������";
            return answer;

        }

        public tblConcerts FindConcertById(int id)
        {
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            var result = concertDB.tblConcerts.Where(b => b.group_id == id);

            if (result.Count() > 0)
                return result.First();
            else
                return null;
        }

        
    }
    
}