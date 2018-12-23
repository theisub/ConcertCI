using System;
using System.Collections.Generic;
using System.Linq;

namespace ConcertCI
{
    public class UserActions
    {
        private ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
        public UserActions()
        {

        }
        public UserActions(ConcertNotifierEntities1 context)
        {
            concertDB = context;

        }
        public List<tblUsers> SelectAllUsers()
        {

            //ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            try
            {
                var query = from b in concertDB.tblUsers
                            orderby b.user_id
                            select b;
                
                return query.ToList();

            }
            catch (Exception e)
            {

                Console.Write(e);
                return null;
            }
           
                /* using (var context = new ConcertNotifierEntities1())
                 {
                     foreach (var user in context.tblUsers)
                         Console.WriteLine(user.user_city + " and " + user.user_id);
                 }*/

                
            
        }

        public string InsertUser(int id, string city)
        {
            //ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            string answer = null;
            try
            {
                var user = new tblUsers();
                user.user_id = id;
                user.user_city = city;
                concertDB.tblUsers.Add(user);
                concertDB.SaveChanges();
                answer = string.Format("ѕоздравл€ем с регистрацией, ваш город {0}", city);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw (new Exception());
            }


            return answer;

        }

        public string AddingCityToUser(int id, string city)
        {
            UserActions userActions = new UserActions();
            string answer = null;
            try
            {
                if (userActions.ContainsUser(id))
                {

                    userActions.UpdateUser(id, city);
                    answer = String.Format("¬аш город обновлен, теперь вы находитесь в городе {0}", city);

                }
                else
                {
                    userActions.InsertUser(id, city);
                    answer = String.Format("¬ы зарегестрированы в городе {0}", city);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception();
            }
            

            return answer;
        }

        public  bool ContainsUser(int id)
        {
            //ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            var result = concertDB.tblUsers.Where(b => b.user_id == id);

            if (result.Count() > 0)
                return true;
            else
                return false;
        }

        public tblUsers FindUser(int id)
        {
            ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            var result = concertDB.tblUsers.Where(b => b.user_id == id);

            if (result.Count() > 0)
                return result.First();
            else
                return null;
        }

        public  void UpdateUser(int id, string city)
        {
                var user = new tblUsers();
                //ConcertNotifierEntities1 concertDB = new ConcertNotifierEntities1();
            
                user = concertDB.tblUsers.Where(b => b.user_id == id).First();
                user.user_city = city;
                concertDB.SaveChanges();
            
        }


        
    }
    
}