using AzFnCRUDSample.Domain;
using AzFnCRUDSample.Service.Common.Interface;
using AzFnCRUDSample.Service.Common.Model;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace AzFnCRUDSample.Service.Menu;

internal class MenuService (IUnitOfWork unitOfWork, ILogger<MenuService> logger) : IMenuService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<MenuService> _logger = logger;

    // get all itemtype
    public async Task<ResponseModel<List<ItemType>>> GetItemTypesAsync()
    {
        try
        {
            var itemTypes = _unitOfWork.Repository<ItemType>().GetAll();
            return ResponseModel<List<ItemType>>.SuccessResponse("ItemTypes", itemTypes.ToList());

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetItemTypesAsync");
            return ResponseModel<List<ItemType>>.FailureResponse("Internal Server Error");
        }
    }

    // create itemtype
    public async Task<ResponseModel<ItemType>> CreateItemTypeAsync(ItemType itemType)
    {
        try
        {
         var item = await _unitOfWork.Repository<ItemType>().AddAsync(itemType);
            await _unitOfWork.SaveAsync();
            return ResponseModel<ItemType>.SuccessResponse("Item Added Successfully", item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateItemTypeAsync");
            return ResponseModel<ItemType>.FailureResponse("internal Server error");
        }
    }

    // get itemtype by id
    public async Task<ResponseModel<ItemType>> GetItemTypeAsync(int id)
    {
        try
        {
            var itemType = await _unitOfWork.Repository<ItemType>().Get(id);
            return ResponseModel<ItemType>.SuccessResponse("Success", itemType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetItemTypeAsync");
            return ResponseModel<ItemType>.FailureResponse("internal Server error");
        }
    }

    // update itemtype
    public async Task<ResponseModel<ItemType>> UpdateItemTypeAsync(ItemType itemType)
    {
        try
        {
            var item = await _unitOfWork.Repository<ItemType>().UpdateAsync(itemType.Id, itemType);
            await _unitOfWork.SaveAsync();
            return ResponseModel<ItemType>.SuccessResponse("Success", item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateItemTypeAsync");
            return ResponseModel<ItemType>.FailureResponse("internal Server error");
        }
    }

    // delete itemtype
    public async Task<ResponseModel<bool>> DeleteItemTypeAsync(int id)
    {
        try
        {
           var result = await _unitOfWork.Repository<ItemType>().Delete(id);
            if(result)
            {
                await _unitOfWork.SaveAsync();
                return ResponseModel<bool>.SuccessResponse("Item Deleted Successfully", true);
            }
            else
            {
                return ResponseModel<bool>.FailureResponse("Item not found");
            }
           
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteItemTypeAsync");
            return ResponseModel<bool>.FailureResponse("Internal Server error!!");
        }


    }
}