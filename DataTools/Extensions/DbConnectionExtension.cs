using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataTools.Extensions
{
    public static class DbConnectionExtension
    {
        //Les methodes permettent de gérer les commandes, les parametres et les transactions

        /// <summary>
        /// Retourne qu'un seul objet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="DbCon"></param>
        /// <param name="Command"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static T QuerySingleOrDefault<T>(this IDbConnection DbCon, string Command, object parameters, IDbTransaction transaction)
        {
            //si la connection est null => delcenchement d'exception
            if (DbCon == null)
            {

                throw new InvalidOperationException("Your Connection must be set");
            }

            T retour = default;
            IDataReader Idr = null;
            //si la connection  est fermée on l'ouvre
            if (DbCon.State == ConnectionState.Closed)
            {

                DbCon.Open();
            }

            //Creation de la commande
            IDbCommand myCommand = DbCon.CreateCommand();
            if (transaction != null)
            {
                // gestion de la transaction
                myCommand.Transaction = transaction;
            }
            try
            {
                //ajout de la commande
                myCommand.CommandText = Command;
                if (parameters != null)
                {
                    //gestion des parametres
                    SetParameters(myCommand, parameters);
                }
                Idr = myCommand.ExecuteReader();

                //pour lire les lignes du reader tant qu'il y en a
                if (Idr.Read())
                {
                    //utilisation pour sortir vers le bon model d'entities
                    retour = MapTo<T>(Idr);
                }

            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message); // permet d'ecrire un message de debug dans le output (que en mode debug)

            }
            finally
            {
                Idr.Close();
            }

            return retour;
        }

        /// <summary>
        /// Retourne une collection d'objet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="DbCon"></param>
        /// <param name="Command"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(this IDbConnection DbCon, string Command, object parameters, IDbTransaction transaction)
        {
            if (DbCon == null)
            {

                throw new InvalidOperationException("Your Connection must be set");
            }

            Type listType = typeof(List<>); // creation du Type d'une liste

            // creation du Type d'une liste d'un type particulier (générique)
            Type constructedListType = listType.MakeGenericType(typeof(T));

            //Creation de la liste grace aux Types créé au-dessus
            List<T> retour = (List<T>)Activator.CreateInstance(constructedListType);

            if (DbCon.State == ConnectionState.Closed)
            {

                DbCon.Open();
            }

            IDbCommand myCommand = DbCon.CreateCommand();
            IDataReader Idr = null;
            if (transaction != null)
            {
                myCommand.Transaction = transaction;
            }
            try
            {
                myCommand.CommandText = Command;
                if (parameters != null)
                {
                    SetParameters(myCommand, parameters);
                }

                Idr = myCommand.ExecuteReader();
                while (Idr.Read())
                {
                    if (typeof(T) == typeof(int))
                    {
                        retour.Add((T)(Idr[Idr.GetName(0)]));
                    }
                    else
                    {

                        retour.Add(MapTo<T>(Idr));
                    }
                }

            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);

            }
            finally
            {
                Idr.Close();
            }

            return retour;
        }

        /// <summary>
        /// Retourne une seule valeur du type de la Primary Key
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="DbCon"></param>
        /// <param name="Command"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static TKey ExecuteScalar<TKey>(this IDbConnection DbCon, string Command, object parameters, IDbTransaction transaction)
        {
            if (DbCon == null)
            {

                throw new InvalidOperationException("Your Connection must be set");
            }

            TKey retour = default;
            if (DbCon.State == ConnectionState.Closed)
            {

                DbCon.Open();
            }

            IDbCommand myCommand = DbCon.CreateCommand();
            if (transaction != null)
            {
                myCommand.Transaction = transaction;
            }
            try
            {
                myCommand.CommandText = Command;
                if (parameters != null)
                {
                    SetParameters(myCommand, parameters);
                }
                //TODO
                retour = (TKey)myCommand.ExecuteScalar();

            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                Debug.WriteLine(e.Message);

            }

            return retour;
        }

        /// <summary>
        /// Permet d'executer une commande qui retourne un bool 
        /// </summary>
        /// <param name="DbCon"></param>
        /// <param name="Command"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static bool Execute(this IDbConnection DbCon, string Command, object parameters, IDbTransaction transaction)
        {
            if (DbCon == null)
            {

                throw new InvalidOperationException("Your Connection must be set");
            }
            bool result = false;

            if (DbCon.State == ConnectionState.Closed)
            {
                DbCon.Open();

            }

            IDbCommand myCommand = DbCon.CreateCommand();
            if (transaction != null)
            {
                myCommand.Transaction = transaction;
            }
            try
            {
                myCommand.CommandText = Command;
                if (parameters != null)
                {
                    SetParameters(myCommand, parameters);
                }

                myCommand.ExecuteNonQuery();


                result = true;
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                Debug.WriteLine(e.Message);

            }

            return result;
        }

        /// <summary>
        /// Permet d'ajouter les parametres à la commande
        /// </summary>
        /// <param name="myCommand"></param>
        /// <param name="parameters"></param>
        private static void SetParameters(IDbCommand myCommand, object parameters)
        {
            try
            {

                if (parameters == null)
                {

                    return;
                }
                if (parameters.GetType() == typeof(ExpandoObject))
                {
                    ICollection<KeyValuePair<string, object>> infos = (ICollection<KeyValuePair<string, object>>)parameters;
                    foreach (KeyValuePair<string, Object> pInfo in infos)
                    {
                        IDbDataParameter param = myCommand.CreateParameter();
                        param.ParameterName = pInfo.Key;
                        param.Value = pInfo.Value ?? DBNull.Value;
                        myCommand.Parameters.Add(param);
                    }
                }
                else
                {
                    foreach (PropertyInfo pInfo in parameters.GetType().GetProperties())
                    {
                        IDbDataParameter param = myCommand.CreateParameter();
                        param.ParameterName = pInfo.Name;
                        param.Value = pInfo.GetValue(parameters) ?? DBNull.Value;
                        myCommand.Parameters.Add(param);

                    }
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }
        }

        private static T MapTo<T>(IDataReader idr)
        {
            //permet de créer une instance du type envoyé en générique (T)
            T retour = Activator.CreateInstance<T>();
            //on fait le tour de chaque champ dans la ligne du reader
            for (int i = 0; i < idr.FieldCount; i++)
            {
                string Name = idr.GetName(i); // recupération du nom
                object value = idr.GetValue(i); // récupération de la valeur
                if (value != DBNull.Value)
                {
                    try
                    {
                        //ajout de la valeur dans la proprieté
                        typeof(T).GetProperty(Name).SetValue(retour, value);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message); // permet d'ecrire un message de debug dans le output (que en mode debug)
                        throw;
                    }
                }
            }
            return retour; // retour de l'objet avec toutes ses propriétés
        }



    }
}
