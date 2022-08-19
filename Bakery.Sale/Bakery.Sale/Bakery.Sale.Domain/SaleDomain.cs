﻿using Bakery.Sale.DomainApi.Model;
using Bakery.Sale.DomainApi.Port;
using Bakery.Sale.Persistence.Adapter.Context;
using Bakery.Sale.Persistence.Adapter.Dto;
using Bakery.Sale.Persistence.Adapter.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Sale.Domain
{
    public class SaleDomain<T> : IRequestSale<T>  where T : Bakery.Sale.DomainApi.Model.SaleEntity
    {

        AppDbContext _dbContext;

        private readonly DbSet<T> table;

        public SaleDomain(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
            table = _dbContext.Set<T>();
        }


        public List<T> GetSale()
        {
            return table.ToList();
        }


        public T GetSaleById(int id)
        {
            return table.Find(id);
        }


        public T AddSale(T dto)
        {
           table.Add(dto);
           _dbContext.SaveChanges();
           return dto;
        }


        public T UpdateSale(int id, T dto)
        {
            var register = table.Where(i => i.Id == id).FirstOrDefault();
            if (register == null) return null;
            register.UserName = dto.UserName;
            register.Invoice = dto.Invoice;
            register.QRCode = dto.QRCode;
            register.Product_Id = dto.Product_Id; 
            register.Quantity=dto.Quantity;
            table.Update(register);
            _dbContext.SaveChanges();
            return dto;
        }

        public T RemoveSaleById(int id)
        {
            StatusDto status = new StatusDto();
            var register = table.Where(i => i.Id == id).FirstOrDefault();
            if (register == null) return null;
            table.Remove(register);
            _dbContext.SaveChanges();
            return register;

        }

        public List<T> RemoveSaleByInvoice(string invoice)
        {
            StatusDto status = new StatusDto();
            var registers = table.Where(i => i.Invoice == invoice).ToList();
            if (registers == null) return null;
            foreach (var register in registers)
            {
                table.Remove(register);
            }
            _dbContext.SaveChanges();
            return registers;

        }


    }
}
