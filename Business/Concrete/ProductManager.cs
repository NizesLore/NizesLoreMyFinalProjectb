﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {   // CONSTRUCTOR INJECTION
       
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            //Business Codes 

            if (product.ProductName.Length<2)
            {
                //magic strings
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {   // iş kodları,if ler var
            // Yetkisi var mı? Evetse DATA ACCESS e diyor ki bana ürünleri verebilirsin

            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Product>> (Messages.MaintenanceTime);
            }
            //return new SuccessDataResult<List<Product>>(_productDal.GetAll(),true,"Ürünler listelendi");
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=> p.CategoryId==id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=> p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>> (_productDal.GetAll(p=> p.UnitPrice>=min && p.UnitPrice<=max ));

        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 15)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());

        }





        //IResult IProductService.Add(Product product)
        //{
        //    throw new NotImplementedException();
        //}

        //IDataResult<List<Product>> IProductService.GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //IDataResult<List<Product>> IProductService.GetAllByCategoryId(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //IDataResult<Product> IProductService.GetById(int productId)
        //{
        //    throw new NotImplementedException();
        //}

        //IDataResult<List<Product>> IProductService.GetByUnitPrice(decimal min, decimal max)
        //{
        //    throw new NotImplementedException();
        //}

        //IDataResult<List<ProductDetailDto>> IProductService.GetProductDetails()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
