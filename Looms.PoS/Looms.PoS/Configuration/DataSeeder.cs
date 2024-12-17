using Bogus;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Persistance;

public class DataSeeder
{
    private readonly AppDbContext _context;

    public DataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        Randomizer.Seed = new Random(123);

        var fakerBusiness = new Faker<BusinessDao>()
            .RuleFor(b => b.Id, f => Guid.NewGuid())
            .RuleFor(b => b.Name, f => f.Company.CompanyName())
            .RuleFor(b => b.OwnerName, f => f.Name.FullName())
            .RuleFor(b => b.Email, f => f.Internet.Email())
            .RuleFor(b => b.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(b => b.StartHour, f => f.Random.Int(0, 12))
            .RuleFor(b => b.EndHour, f => f.Random.Int(13, 23))
            .RuleFor(b => b.IsDeleted, f => f.Random.Bool());  // do you want some of them being deleted?

        var fakerUser = new Faker<UserDao>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Name, f => f.Name.FullName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.Role, f => f.PickRandom<UserRole>())
            .RuleFor(u => u.IsDeleted, f => f.Random.Bool());

        var fakerTax = new Faker<TaxDao>()
            .RuleFor(t => t.Id, f => Guid.NewGuid())
            .RuleFor(t => t.Name, f => f.Commerce.ProductName())//i hope its logical
            .RuleFor(t => t.Percentage, f => f.Random.Int(0, 50))
            .RuleFor(t => t.TaxCategory, f => f.PickRandom<TaxCategory>())
            .RuleFor(t => t.StartDate, f => f.Date.Past().ToUniversalTime())
            .RuleFor(t => t.EndDate, f => f.Date.Future().ToUniversalTime())
            .RuleFor(t => t.IsDeleted, f => f.Random.Bool());

        var fakerDiscount = new Faker<DiscountDao>()
            .RuleFor(d => d.Id, f => Guid.NewGuid())
            .RuleFor(d => d.Name, f => f.Commerce.ProductName())//i hope its logical
            .RuleFor(d => d.DiscountType, f => f.PickRandom<DiscountType>())
            .RuleFor(d => d.Value, f => f.Random.Decimal(5,50)) 
            .RuleFor(d => d.Target, f => f.PickRandom<DiscountTarget>())
            .RuleFor(d => d.StartDate, f => f.Date.Past().ToUniversalTime())
            .RuleFor(d => d.EndDate, f => f.Date.Future().ToUniversalTime())
            .RuleFor(d => d.IsDeleted, f => f.Random.Bool());

        var fakerService = new Faker<ServiceDao>()
            .RuleFor(s => s.Id, f => Guid.NewGuid())
            .RuleFor(s => s.Name, f => f.Commerce.ProductName())
            .RuleFor(s => s.Category, f => f.Commerce.Categories(1)[0])
            .RuleFor(s => s.Description, f => f.Lorem.Paragraph())
            .RuleFor(s => s.Price, f => f.Random.Decimal(10, 100))
            .RuleFor(s => s.DurationMin, f => f.Random.Int(30, 120))
            .RuleFor(s => s.IsDeleted, f => f.Random.Bool());

        var fakerReservation = new Faker<ReservationDao>()
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.CustomerName, f => f.Name.FullName())
            .RuleFor(r => r.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(r => r.Email, f => f.Internet.Email())
            .RuleFor(r => r.AppointmentTime, f => f.Date.Future().ToUniversalTime())
            .RuleFor(r => r.Status, f => f.PickRandom<ReservationStatus>())
            .RuleFor(r => r.Comment, f => f.Lorem.Sentence())
            .RuleFor(r => r.IsDeleted, f => f.Random.Bool());

        var fakerRefund = new Faker<RefundDao>()
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Amount, f => f.Random.Decimal(10, 50)) 
            .RuleFor(r => r.RefundReason, f => f.Lorem.Sentence())
            .RuleFor(r => r.RefundStatus, f => f.PickRandom<RefundStatus>())
            .RuleFor(r => r.CreatedAt, f => DateTime.UtcNow)
            .RuleFor(r => r.ProcessedAt, f => f.Random.Bool() ? f.Date.Past().ToUniversalTime() : null);

        var fakerProductVariation = new Faker<ProductVariationDao>()
            .RuleFor(pv => pv.Id, f => Guid.NewGuid())
            .RuleFor(pv => pv.Price, f =>f.Random.Decimal(10, 100)) 
            .RuleFor(pv => pv.Quantity, f => f.Random.Int(1, 100)) 
            .RuleFor(pv => pv.VariationName, f => f.Commerce.ProductName())
            .RuleFor(pv => pv.IsDeleted, f => f.Random.Bool());

        var fakerProduct = new Faker<ProductDao>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
            .RuleFor(p => p.Price, f =>f.Random.Decimal(10, 100)) 
            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 100)) 
            .RuleFor(p => p.IsDeleted, f => f.Random.Bool());

        var fakerPaymentTerminal = new Faker<PaymentTerminalDao>()
            .RuleFor(pt => pt.Id, f => Guid.NewGuid())
            .RuleFor(pt => pt.Name, f => f.Company.CompanyName())
            .RuleFor(pt => pt.ExternalId, f => f.Random.AlphaNumeric(10))
            .RuleFor(pt => pt.IsActive, f => f.Random.Bool())
            .RuleFor(pt => pt.IsDeleted, f => f.Random.Bool());

        var fakerPaymentProvider = new Faker<PaymentProviderDao>()
            .RuleFor(pp => pp.Id, f => Guid.NewGuid())
            .RuleFor(pp => pp.Name, f => f.Company.CompanyName())
            .RuleFor(pp => pp.Type, f => f.PickRandom<PaymentProviderType>())
            .RuleFor(pp => pp.ExternalId, f => f.Random.AlphaNumeric(10))
            .RuleFor(pp => pp.ApiSecret, f => f.Internet.Password())
            .RuleFor(pp => pp.WebhookSecret, f => f.Internet.Password())
            .RuleFor(pp => pp.IsActive, f => f.Random.Bool())
            .RuleFor(pp => pp.IsDeleted, f => f.Random.Bool());

        var fakerPayment = new Faker<PaymentDao>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Amount, f => f.Random.Decimal(10, 100)) 
            .RuleFor(p => p.PaymentMethod, f => f.PickRandom<PaymentMethod>())
            .RuleFor(p => p.Tip, f => f.Random.Decimal(0, 20)) 
            .RuleFor(p => p.Status, f => f.PickRandom<PaymentStatus>())
            .RuleFor(p => p.ExternalId, f => f.Random.Bool() ? f.Random.AlphaNumeric(10): null)
            .RuleFor(p => p.IsDeleted, f => f.Random.Bool());

        var fakerGiftCard = new Faker<GiftCardDao>()
            .RuleFor(gc => gc.Id, f => Guid.NewGuid())
            .RuleFor(gc => gc.Code, f => f.Random.AlphaNumeric(10))
            .RuleFor(gc => gc.InitialBalance, f => f.Random.Decimal(50, 500))
            .RuleFor(gc => gc.CurrentBalance, f => f.Random.Decimal(0, 500))
            .RuleFor(gc => gc.ExpiryDate, f => f.Date.Future().ToUniversalTime())
            .RuleFor(gc => gc.IsActive, f => f.Random.Bool())
            .RuleFor(gc => gc.IsDeleted, f => f.Random.Bool());

        var fakerOrder = new Faker<OrderDao>()
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.Status, f => f.PickRandom<OrderStatus>())
            .RuleFor(o => o.IsDeleted, f => f.Random.Bool());
        
        var fakerOrderItem = new Faker<OrderItemDao>()
            .RuleFor(oi => oi.Id, f => Guid.NewGuid())
            .RuleFor(oi => oi.Quantity, f => f.Random.Int(1, 100))
            .RuleFor(oi => oi.Price, f => f.Random.Decimal(10, 100))
            .RuleFor(oi => oi.Tax, f => f.Random.Decimal(0, 10));


        //Relationships and generating data
        var businesses = fakerBusiness.Generate(1).ToList();

        var taxes = new List<TaxDao>();
        foreach (var business in businesses)
        {
            taxes = fakerTax.Generate(5).Select(t => t with { BusinessId = business.Id }).ToList();
            foreach (var tax in taxes)
            {
                business.Taxes.Add(tax);
            }
        }

        var users = new List<UserDao>();
        foreach (var business in businesses)
        {
            users = fakerUser.Generate(15).Select(u => u with { BusinessId = business.Id }).ToList();
            foreach (var user in users)
            {
                business.Users.Add(user);
            }
        }
        var services = new List<ServiceDao>();
        foreach (var business in businesses)
        {
            services = fakerService.Generate(5).Select(s => s with { BusinessId = business.Id, TaxId = taxes[new Faker().Random.Int(0, taxes.Count - 1)].Id }).ToList();
            foreach (var service in services)
            {
                business.Services.Add(service);
                var tax = taxes.First(t => t.Id == service.TaxId);
                tax.Services.Add(service);
            }
        }

        var reservations = new List<ReservationDao>();
        var employees = users.Where(u => u.Role == UserRole.Employee).ToList();
        foreach (var service in services)
        {
            reservations = fakerReservation.Generate(5).Select(r => r with { ServiceId = service.Id, EmployeeId = employees[new Faker().Random.Int(0, employees.Count - 1)].Id }).ToList();
            foreach (var reservation in reservations)
            {
                service.Reservations.Add(reservation);
                var user = users.First(u => u.Id == reservation.EmployeeId);
                user.Reservations.Add(reservation);
            }
        }

        var giftcards = new List<GiftCardDao>();
        foreach (var business in businesses)
        {
            giftcards = fakerGiftCard.Generate(5).Select(gc => gc with { BusinessId = business.Id, IssuedById = users[new Faker().Random.Int(0, users.Count - 1)].Id }).ToList();
            foreach(var giftcard in giftcards)
            {
                business.GiftCards.Add(giftcard);
                var user = users.First(u => u.Id == giftcard.IssuedById);
                user.GiftCards.Add(giftcard);
            }
        }

        var products = new List<ProductDao>();
        foreach (var business in businesses)
        {
            products = fakerProduct.Generate(20).Select(p => p with { BusinessId = business.Id, TaxId = taxes[new Faker().Random.Int(0, taxes.Count - 1)].Id }).ToList();
            foreach (var product in products)
            {
                business.Products.Add(product);
                var tax = taxes.First(t => t.Id == product.TaxId);
                tax.Products.Add(product);
            }
        }

        var productVariations = new List<ProductVariationDao>();
        foreach (var product in products)
        {
            productVariations = fakerProductVariation.Generate(5).Select(pv => pv with { ProductId = product.Id }).ToList();
            foreach (var productVariation in productVariations)
            {
                product.Variations.Add(productVariation);
            }
        }

        var discounts = new List<DiscountDao>();
        foreach (var business in businesses)
        {
            discounts = fakerDiscount.Generate(5).Select(d => d with { BusinessId = business.Id, ProductId = new Faker().Random.Bool() ? products[new Faker().Random.Int(0, products.Count - 1)].Id : null}).ToList();
            foreach (var discount in discounts)
            {
                business.Discounts.Add(discount);
                if(discount.ProductId.HasValue)
                {
                    var product = products.First(p => p.Id == discount.ProductId);
                    product.Discounts.Add(discount);
                }
            }
        }

        var orders = new List<OrderDao>();
        foreach (var user in users)
        {
            orders = fakerOrder.Generate(5).Select(o => o with 
            { 
                UserId = user.Id, 
                BussinessId = user.BusinessId, 
                DiscountId = new Faker().Random.Bool() ? discounts[new Faker().Random.Int(0, discounts.Count - 1)].Id : null
            }).ToList();
            foreach (var order in orders)
            {
                user.Orders.Add(order); 
                var business = businesses.First(b => b.Id == order.BussinessId);
                business.Orders.Add(order);
                if(order.DiscountId.HasValue)
                {
                    var discount = discounts.First(d => d.Id == order.DiscountId);
                    discount.Orders.Add(order);
                }
            }
        }

        var orderItems = new List<OrderItemDao>();
        foreach(var order in orders)
        {
            orderItems = fakerOrderItem.Generate(5).Select(oi => oi with 
            { 
                OrderId = order.Id, 
                ProductId = new Faker().Random.Bool() ? products[new Faker().Random.Int(0, products.Count - 1)].Id : null,
                ProductVariationId = new Faker().Random.Bool() ? productVariations[new Faker().Random.Int(0, productVariations.Count - 1)].Id : null,
                ServiceId = new Faker().Random.Bool() ? services[new Faker().Random.Int(0, services.Count - 1)].Id : null,
                DiscountId = new Faker().Random.Bool() ? discounts[new Faker().Random.Int(0, discounts.Count - 1)].Id : null

            }).ToList();
            foreach (var orderItem in orderItems)
            {
                order.OrderItems.Add(orderItem);
                if (orderItem.ProductId.HasValue)
                {
                    var product = products.First(p => p.Id == orderItem.ProductId);
                    product.OrderItems.Add(orderItem);
                }
                if(orderItem.ProductVariationId.HasValue)
                {
                    var productVariation = productVariations.First(pv => pv.Id == orderItem.ProductVariationId);
                    productVariation.OrderItems.Add(orderItem); 
                }
                if(orderItem.ServiceId.HasValue)
                {
                    var service = services.First(s => s.Id == orderItem.ServiceId);
                    service.OrderItems.Add(orderItem);
                }
                if(orderItem.DiscountId.HasValue)
                {
                    var discount = discounts.First(d => d.Id == orderItem.DiscountId);
                    discount.OrderItems.Add(orderItem);
                }
            }
        }

        var paymentProviders = new List<PaymentProviderDao>();
        foreach(var business in businesses)
        {
            paymentProviders = fakerPaymentProvider.Generate(5).Select(pp => pp with { BusinessId = business.Id }).ToList();
            foreach (var paymentProvider in paymentProviders)
            {
                business.PaymentProviders.Add(paymentProvider);
            }
        }

        var paymentTerminals = new List<PaymentTerminalDao>();
        foreach (var paymentProvider in paymentProviders)
        {
            paymentTerminals = fakerPaymentTerminal.Generate(5).Select(pt => pt with { PaymentProviderId = paymentProvider.Id }).ToList();
            foreach (var paymentTerminal in paymentTerminals)
            {
                paymentProvider.PaymentTerminals.Add(paymentTerminal);
            }
        }

        var payments = new List<PaymentDao>();
        foreach(var order in orders)
        {
            payments = fakerPayment.Generate(5).Select(p => p with
            {
                OrderId = order.Id,
                GiftCardId = new Faker().Random.Bool() ? giftcards[new Faker().Random.Int(0, giftcards.Count - 1)].Id : (Guid?)null,
                PaymentTerminalId = new Faker().Random.Bool() ? paymentTerminals[new Faker().Random.Int(0, paymentTerminals.Count - 1)].Id : (Guid?)null
            }).ToList();
            foreach (var payment in payments)
            {
                order.Payments.Add(payment);
                if (payment.GiftCardId.HasValue)
                {
                    var giftcard = giftcards.First(gc => gc.Id == payment.GiftCardId);
                    giftcard.Payments.Add(payment);
                }
                if (payment.PaymentTerminalId.HasValue)
                {
                    var paymentTerminal = paymentTerminals.First(pt => pt.Id == payment.PaymentTerminalId);
                    paymentTerminal.Payments.Add(payment);
                }
            }
        }

        var refunds = new List<RefundDao>();
        foreach(var order in orders)
        {
            refunds = fakerRefund.Generate(5).Select(r => r with { OrderId = order.Id, PaymentId = payments[new Faker().Random.Int(0, payments.Count - 1)].Id, UserId = users[new Faker().Random.Int(0, users.Count - 1)].Id}).ToList();
            foreach (var refund in refunds)
            {
                order.Refunds.Add(refund);
                var user = users.First(u => u.Id == refund.UserId);
                user.Refunds.Add(refund);
                var payment = payments.First(p => p.Id == refund.PaymentId);
                payment.Refunds.Add(refund);
            }
        }

        _context.Businesses.AddRange(businesses);
        _context.Taxes.AddRange(taxes);
        _context.Users.AddRange(users);
        _context.Services.AddRange(services);
        _context.Reservations.AddRange(reservations);
        _context.GiftCards.AddRange(giftcards);
        _context.Orders.AddRange(orders);
        _context.Payments.AddRange(payments);
        _context.Refunds.AddRange(refunds);

        _context.SaveChanges();
    }
}