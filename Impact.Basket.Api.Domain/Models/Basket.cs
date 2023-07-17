using System.Collections.Concurrent;

namespace Impact.Basket.Api.Domain.Models
{
    /// <summary>
    /// The `Basket` class represents a basket that can hold multiple `BasketItem` objects.
    /// It allows adding and removing items from the basket.
    /// </summary>
    public class Basket
    {
        private BasketStatus _status;
        /// <summary>
        /// Gets the unique identifier of the basket.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the dictionary of items in the basket. The dictionary key is the product ID,
        /// and the value is the corresponding `BasketItem` object.
        /// </summary>
        public ConcurrentDictionary<int, BasketItem> Items { get; }

        public BasketStatus Status => _status;

        /// <summary>
        /// Initializes a new instance of the `Basket` class with a new unique identifier
        /// and an empty dictionary of items.
        /// </summary>
        public Basket()
        {
            Id = Guid.NewGuid();
            Items = new ConcurrentDictionary<int, BasketItem>();
            _status = BasketStatus.Open;
        }

        /// <summary>
        /// Initializes a new instance of the `Basket` class with the specified identifier and items.
        ///
        /// <param name="id">The unique identifier of the basket.</param>
        /// <param name="items">The dictionary of items in the basket.</param>
        /// </summary>
        public Basket(Guid id, ConcurrentDictionary<int, BasketItem> items, BasketStatus BasketStatus)
        {
            Id = id;
            Items = items;
            _status = BasketStatus;
        }

        /// <summary>
        /// Adds an item to the basket. If an item with the same product ID already exists in the basket,
        /// the quantity of the existing item is increased. Otherwise, the new item is added to the basket.
        /// </summary>
        /// <param name="item">The `BasketItem` to add to the basket.</param>
        public void AddItem(BasketItem item)
        {
            if (!Status.Equals(BasketStatus.Open))
                throw new InvalidOperationException($"Products cannot be added to a non-opened basket");

            if (Items.TryGetValue(item.Product.Id, out BasketItem existingItem))
            {
                existingItem.IncreaseQuantity(item.Quantity);
                Items[item.Product.Id] = existingItem;
            }
            else
                Items[item.Product.Id] = item;
        }

        /// <summary>
        /// Removes an item from the basket. If an item with the specified product ID exists in the basket,
        /// the quantity of the existing item is decreased. If the quantity becomes zero, the item is removed
        /// from the basket. Otherwise, the updated item is stored in the basket.
        /// </summary>
        /// <param name="item">The `BasketItem` to remove from the basket.</param>
        public void RemoveItem(BasketItem item)
        {
            if (!Status.Equals(BasketStatus.Open))
                throw new InvalidOperationException($"Products cannot be removed from a non-opened basket");
            else
            {
                if (Items.TryGetValue(item.Product.Id, out BasketItem existingItem))
                {
                    if (existingItem.Quantity == 1)
                        Items.Remove(item.Product.Id, out _);
                    else
                    {
                        existingItem.DecreaseQuantity();
                        Items[item.Product.Id] = existingItem;
                    }
                }
            }

        }

        /// <summary>
        /// Removes all Items from the basket and update it with the new set of items
        /// from the basket. Otherwise, the updated item is stored in the basket.
        /// </summary>
        /// <param name="items">The `BasketItem` list to replace the existing one.</param>
        public void UpdateWith(IEnumerable<BasketItem> items)
        {
            this.Items.Clear();
            foreach (var item in items)
            {
                this.Items.TryAdd(item.Product.Id, item);
            }
        }

        /// <summary>
        /// Set the BasketStatus of the basket to ordered. After that, no products can be added to or removed from a Basket
        /// </summary>
        public Basket CloseBasket()
        {
            _status = BasketStatus.Ordered;

            return this;
        }
    }
}
