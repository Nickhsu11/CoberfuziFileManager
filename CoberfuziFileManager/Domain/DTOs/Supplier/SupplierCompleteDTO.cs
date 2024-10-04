using System;
using System.Collections;
using System.Collections.Generic;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.DTOs;

public class SupplierCompleteDTO
{
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new Exception("Name cannot be empty");
            
            _name = value;
        }
    }

    private string _phone;
    public string Phone
    {
        get => _phone;
        set
        {

            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Phone cannot be empty");
            
            if (value.Length > 13 || value.Length < 8)
                throw new Exception("Phone must be between 9 and 13 characters");
            
            _phone = value;
        }
    }
    
    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Email cannot be empty");
            
            if (!value.Contains("@"))
                throw new Exception("Email is not valid, missing @");
            
            if (value.Length > 200)
                throw new Exception("Email must be less then 200 characters");
            
            _email = value;
        }
    }

    private string _postCode;
    public string PostCode
    {
        get => _postCode;
        set
        {
            
            if(string.IsNullOrWhiteSpace(value))
                throw new Exception("PostCode cannot be empty");
            
            if(value.Length != 8)
                throw new Exception("PostCode must be exactly 8 characters");
            
            if( !value[4].Equals('-') )
                throw new Exception("PostCode must start with '-'");
            
            _postCode = value;
        }
    }
    
    private string _address;
    public string Address
    {
        get => _address;
        set
        {
            
            if(string.IsNullOrWhiteSpace(value))
                throw new Exception("Address cannot be empty");
            
            if(value.Length > 200 )
                throw new Exception("Address must be less then 200 characters");
            
            _address = value;
        }
    }

    private int _nif;
    public int Nif
    {
        get => _nif;
        set
        {
            
            if(value % (10^9) != 0)
                throw new Exception("NIF must be exactly 9 characters");
            
            _nif = value;
        }
    }

    private int _supplierID;

    public int SupplierID
    {
        get => _supplierID;
        set
        {
            
            if( value < 0 ) 
                throw new Exception("Supplier ID cannot be negative");
            
            _supplierID = value;
        }
    }
    
}