using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Sale.DomainApi
{
    public abstract class Entity
    {
        #region Properties

        /// <summary>
        /// Defines creation date for domain entity.
        /// </summary>
        public virtual DateTime CreationDate { get; set; }
        /// <summary>
        /// Defines modified date for domain entity.
        /// </summary>
        public virtual DateTime? ModifiedDate { get; set; }

        #endregion
    }

    public abstract class Entity<TEntityId> : Entity
    {
        #region Members & Properties

        /// <summary>
        /// Defines the identifier for domain class
        /// </summary>
        public virtual TEntityId Id { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Use to update creation/modified date for domain entity
        /// </summary>
        public virtual void UpdateModifiedDate()
        {
            if (CreationDate != default)
            {
                ModifiedDate = DateTime.UtcNow;
            }
        }

        #endregion


    }
}
