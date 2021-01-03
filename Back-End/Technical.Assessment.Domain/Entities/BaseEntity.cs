using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technical.Assessment.Domain.Entities
{
    /// <summary>
    /// Base Entity
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    [Serializable]
    public class BaseEntity<T> : DomainEntity, IEntityBase<T>
    {
        /// <summary>
        /// Id Model
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        /// <summary>
        /// DateCreated
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// DateModified
        /// </summary>
        public DateTime? DateModified { get; set; }
    }
}
