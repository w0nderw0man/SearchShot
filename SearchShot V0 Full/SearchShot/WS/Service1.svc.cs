using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace WS
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }
/*
        public string SetTest(string test)
        {
            Console.WriteLine("fonction setTest entree");
            try
            {
                Console.WriteLine("try");
               
                string requeteInsertion = "insert into dbo.testTable (test) VALUES (@insert)";
                string chaineConnection = "Data Source=(LocalDb)\v11.0;Initial Catalog=SearchShoot;Integrated Security=True;Pooling=False";
                var sqlconnection = new SqlConnection(chaineConnection);
                Console.WriteLine("open cnt");

                var rq = new SqlCommand(requeteInsertion, sqlconnection);
                rq.Parameters.AddWithValue("@insert", test);
                sqlconnection.Open();
                int i = rq.ExecuteNonQuery();
                sqlconnection.Close();
                Console.WriteLine("close cnt");

                return "ok"+i;
            }
            catch (Exception)
            {
                return "merde";
                
            }
            
            
        }

        public string GetTest()
        {
            string[] plop = null;

            try
            {
                string requeteSeekToken = "SELECT test from dbo.testTable";

                string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";

                var sqlconnection = new SqlConnection(chaineConnection);
                sqlconnection.Open();
                var rq = new SqlCommand(requeteSeekToken, sqlconnection);

                try
                {
                    int i = 0;
                    SqlDataReader reader = rq.ExecuteReader();
                    while (reader.Read())
                    {
                        plop[i] = (string)reader[0];
                        i++;
                    }
                    sqlconnection.Close();
                    return plop[0];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            catch (Exception)
            {
                return "merde";
            }

            
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public int Inscription(string password, string login, int level, int score, int connexion, Image picture,
            string token, string mail, DateTime date_insc, DateTime last_connect)
        {
            //refaire la requete
            string requeteInsertion =
                "insert into dbo.Users (test) VALUES (@password, @login, @level, @score, @connexion, @picture, @token, @mail, @sate_insc, @last_connect)";
            string chaineConnection = "Data Source=(LocalDb)\v11.0;Initial Catalog=SearchShoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);
            Console.WriteLine("open cnt");

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@password", password);
            rq.Parameters.AddWithValue("@login", login);
            rq.Parameters.AddWithValue("@level", level);
            rq.Parameters.AddWithValue("@score", score);
            rq.Parameters.AddWithValue("@connexion", connexion);
            rq.Parameters.AddWithValue("@picture", picture);
            rq.Parameters.AddWithValue("@token", token);
            rq.Parameters.AddWithValue("@mail", mail);
            rq.Parameters.AddWithValue("@last_connect", last_connect);

            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
            Console.WriteLine("close cnt");

            return  i;
        }*/

        public bool Authentification(string login, string mail, string password)
        {
            string recup = null;
            try
            {
                string requeteSeekToken = "SELECT mdp from dbo.Users WHERE login = @login OR mail = @mail AND password = @password";

                string chaineConnection = "Data Source=(LocalDb)\v11.0;Initial Catalog=SearchShoot;Integrated Security=True;Pooling=False";

                var sqlconnection = new SqlConnection(chaineConnection);
                sqlconnection.Open();
                var rq = new SqlCommand(requeteSeekToken, sqlconnection);
                rq.Parameters.AddWithValue("@password", password);
                rq.Parameters.AddWithValue("@login", login);
                rq.Parameters.AddWithValue("@mail", mail);
                try
                {
                    SqlDataReader reader = rq.ExecuteReader();
                    while (reader.Read())
                    {
                        recup = (string)reader[0];
                    }
                    sqlconnection.Close();
                    if (recup != null|| recup != "")
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
/*
        public void LastLevelDate(int id_joueur)
        {
            //refaire la requete
            string requeteInsertion =
                "update date_niv = "+DateTime.Today;
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);
            Console.WriteLine("open cnt");

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
            Console.WriteLine("close cnt");

            
        }

        public void SetLastConnection(int id_joueur)
        {
            //refaire la requete
            string requeteInsertion =
                "update last_con = " + DateTime.Today;
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);
            Console.WriteLine("open cnt");

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
            Console.WriteLine("close cnt");
        }

        public void setNiveauEnd(int lvl, int id_joueur)
        {
            //refaire la requete
            //requete de recuperation du champs niveau
            //requete d'update dans le champs niveau
            //recuperation des anciens niveaux
            //concatenation avec le nouveau niveau
            //update du champs niveaux avec les nouveaux niveaux mis a jour

            string requeteInsertion =
                "update date_niv = " + DateTime.Today;
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);
            Console.WriteLine("open cnt");

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
            Console.WriteLine("close cnt");
        }

        public int[] getNiveauEnd(int id_joueur)
        {
            int[] lvlend = null;
            //refaire la requete
            string requeteInsertion =
                "select end_niv from utilisateur where id = @idjoueur";
            string chaineConnection =
                "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            try
            {
                int i = 0;
                SqlDataReader reader = rq.ExecuteReader();
                while (reader.Read())
                {
                    lvlend[i] = (int) reader[0];
                    i++;
                }
                sqlconnection.Close();
                return lvlend;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public void UpdateScore(int id_joueur, int score)
        {
            //refaire la requete
            string requeteInsertion =
                "update score = @score From dbo.utilisateur Where id = @id_joueur";
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@score", score);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
        }

        public void setEnLigne(int id_joueur)
        {
            //refaire la requete
            string requeteInsertion =
                "update connexion = 1 From dbo.User Where id = @id_joueur";
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
        }

        public void setHorsLigne(int id_joueur)
        {
            
            //refaire la requete
            string requeteInsertion =
                "update connexion = 0 From dbo.User Where id = @id_joueur";
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
        }

        public bool isEnLigne(int id_joueur)
        {
            bool co = false;
            string requeteInsertion ="select connexion From dbo.User Where id = @id_joueur";
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            try
            {
                SqlDataReader reader = rq.ExecuteReader();
                while (reader.Read())
                {
                    co = (bool)reader[0];
                }
                sqlconnection.Close();
                return co;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public string GetScore(int id_joueur)
        {
            string score = "";
            string requeteSelectScore = "select score From dbo.User Where id = @id_joueur";
            
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteSelectScore, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            try
            {
                SqlDataReader reader = rq.ExecuteReader();
                while (reader.Read())
                {
                    score = (string)reader[0];
                }
                sqlconnection.Close();
                return score;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }*/
    }
}
