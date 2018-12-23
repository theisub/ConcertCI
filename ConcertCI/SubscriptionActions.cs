using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ConcertCI
{
    public class SubscriptionActions
    {
        public  DbSet<tblSubscriptions> SelectAllSubscriptions()
        {
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            return concertDB.tblSubscriptions;
        }

        public  IList<tblSubscriptions> SelectSubscriptions(int user)
        {
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            return concertDB.tblSubscriptions.Where(b => b.user_id == user).ToList();
        }
        public string DeleteAllSubscriptions(int user)
        {
            string answer = "��� ������ �������� ������";

            try
            {
                ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
                var result = concertDB.tblSubscriptions.Where(b => b.user_id == user);

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        concertDB.tblSubscriptions.Remove(item);
                    }
                    concertDB.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return answer;

        }
        public string DeleteSubscription(int user, string name)
        {
            GroupActions groupActions = new GroupActions();
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            var group = groupActions.FindGroupByName(name);

            string answer = null;
            var result = concertDB.tblSubscriptions.Where(b => (b.user_id == user) && (b.group_id == group.group_id));

            try
            {
                if (result.ToList().Count()>0)
                    foreach (var item in result)
                        concertDB.tblSubscriptions.Remove(item);
                concertDB.SaveChanges();
                answer = "����������� ������� ������";
            }
            catch (Exception e)
            {
                answer = "� ��� ��� �������� � ����� ������";
                Console.WriteLine(e);
                return answer;
            }




            return answer;
        }
            

         


        

        public string ShowSubList(int user)
        {
            UserActions userActions = new UserActions();
            GroupActions groupActions = new GroupActions();

            string answer = null;


            if (userActions.ContainsUser(user))
            {
                var select = SelectSubscriptions(user);
                if (select != null)
                {
                    int i = 1;
                    foreach (var item in select)
                    {
                        var id = item.group_id;
                        var find = groupActions.FindGroupById(id);
                        if (find != null)
                        {
                            var name = find.group_name;
                            answer += string.Format("{0}) {1}\n", i++, name);
                        }
                        else
                        {
                            answer = "�������� ������ � ������� ������� \"/add <������>\".";
                        }
                    }

                    if (select.Count() ==0)
                    {
                        answer = "�������� ������ � ������� ������� \"/add <������>\".";
                    }

                }
               
            }
            else
            {
                answer = "������������ � ����� id �� ������. �������� ����� � ������� \"/city";
            }

            return answer;

        }

        public IQueryable<tblSubscriptions> DeleteSubscription(int user, int group_id)
        {
            GroupActions groupActions = new GroupActions();
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            var group = groupActions.FindGroupById(group_id);
            if (group == null)
                return null;


            var Result = concertDB.tblSubscriptions.Where(b => (b.user_id == user) && (b.group_id == group.group_id));

            if (Result != null)
            {
                foreach (var item in Result)
                    concertDB.tblSubscriptions.Remove(item);
                concertDB.SaveChanges();
            }

            return Result;


        }
        public  void InsertSubscription(int user, int group)
        {
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            var sub = new tblSubscriptions();
            var subs = SelectAllSubscriptions();
            sub.group_id = group;
            sub.user_id = user;
            sub.subscription_id = subs.Count() + 1;
            concertDB.tblSubscriptions.Add(sub);
            concertDB.SaveChanges();
        }

        public  bool ContainsSubscription(string name, int user)
        {
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            GroupActions groupActions = new GroupActions();
            var group = groupActions.FindGroupByName(name);
            var result = concertDB.tblSubscriptions.Where(b => (b.group_id == group.group_id) && (b.user_id == user));

            if (result.Count() > 0)
                return true;
            else
                return false;
        }
        
    }

}