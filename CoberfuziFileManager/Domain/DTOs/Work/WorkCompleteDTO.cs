using System;

namespace CoberfuziFileManager.Domain.DTOs;

public class WorkCompleteDTO
{

    private int _workID;
    public int WorkID
    {
        get => _workID;
        set
        {
            
            if(value < 0)
                throw new Exception("Work ID cannot be less than 0");
            
            _workID = value;
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

    private int _clientID;
    public int ClientID
    {
        get => _clientID;
        set
        {
            
            if ( value < 0 ) 
                throw new Exception("ClientID cannot be less than 0");

            _clientID = value;
        }
    }



}