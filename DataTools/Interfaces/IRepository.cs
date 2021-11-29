using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTools.Interfaces
{
    /// <summary>
    /// Interface qui conditionne l'implementation de nos repository
    /// </summary>
    /// <typeparam name="T">L'entité du repository</typeparam>
    /// <typeparam name="TKey">Le type de l'identifiant</typeparam>
    public interface IRepository<T, TKey, TType> where T : IEntity<TKey, TType>, new()
                                          where TKey : IPK<TType> // par valeur (types primitifs) Id de type Tkey (type de la Primary Key)
    {


        /// <summary>
        /// Methode qui retourne une entité unique par rapport à son Id
        /// </summary>
        /// <param name="id">l'identifiant</param>
        /// <returns>A <seealso cref="{T}"/> object correspondant à l'Id ou au default(T)/> /></returns>
        T GetOne(TKey id);

        /// <summary>
        /// Methode qui retourne toutes les entités du <seealso cref="{T}"/>Type
        /// </summary>
        /// <returns>Une collection d'entité <seealso cref="IEnumerable{T}"/></returns>
        IEnumerable<T> GetAll();


        /// <summary>
        /// Methode pour ajouter une entité
        /// </summary>
        /// <param name="Entity">L'entité à ajouter</param>
        /// <returns>True si c'est ok</returns>
        int Add(T Entity);


        /// <summary>
        /// Methode trouver une entité à partir d'un critère
        /// </summary>
        /// <param name="criteria">un Func (délégué générique avec retour) pour tester si les records matchent</param>
        /// <returns>Un entité <seealso cref="{T}"/></returns>
        //T Find(Func<T, bool> criteria);


        /// <summary>
        /// Methode pour updater une entité
        /// </summary>
        /// <param name="Entity">L'entité à updater</param>
        /// <returns>True si c'est ok</returns>
        int Update(T Entity);


        /// <summary>
        /// Methode pour supprimer une entité
        /// </summary>
        /// <param name="Entity">L'entité à supprimer</param>
        /// <returns>True si c'est ok</returns>
        int Delete(TKey id);
    }
}

