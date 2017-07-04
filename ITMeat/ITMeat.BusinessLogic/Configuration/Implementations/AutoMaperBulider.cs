using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Models;
using MimeKit;

namespace ITMeat.BusinessLogic.Configuration.Implementations
{
    public static class AutoMapperBuilder
    {
        public static void Build()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<MimeMessage, EmailMessageModel>()
                    .ForMember(x => x.From, y => y.MapFrom(x => x.From.Mailboxes.FirstOrDefault().Address))
                    .ForMember(x => x.Recipient, y => y.MapFrom(x => x.To.Mailboxes.FirstOrDefault().Address))
                    .ForMember(x => x.Message, y => y.MapFrom(x => x.HtmlBody));

                config.CreateMap<MealModel, Meal>().MaxDepth(1);
                config.CreateMap<Meal, MealModel>().MaxDepth(1);

                config.CreateMap<UserModel, User>().MaxDepth(1);
                config.CreateMap<User, UserModel>().MaxDepth(1);

                config.CreateMap<OrderModel, Order>().MaxDepth(1);
                config.CreateMap<Order, OrderModel>().MaxDepth(1);

                config.CreateMap<UserOrderModel, UserOrder>().MaxDepth(1);
                config.CreateMap<UserOrder, UserOrderModel>().MaxDepth(1);

                config.CreateMap<PubModel, Pub>().MaxDepth(1);
                config.CreateMap<Pub, PubModel>().MaxDepth(1);

                config.CreateMissingTypeMaps = true;
            });
        }
    }
}