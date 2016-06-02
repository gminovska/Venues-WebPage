﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVenueRegistrationLoginService" in both code and config file together.
[ServiceContract]
public interface IVenueRegistrationLoginService
{

    [OperationContract]
    bool RegisterVenue(Venue v, string VenueUserName, string VenuePassword);

    [OperationContract]
    int VenueLogin(string userName, string password);

    [OperationContract]
    bool AddArtist(Artist a);

    [OperationContract]
    bool AddShow(Show s);

    [OperationContract]
    bool AddShowDetail(ShowDetail sd);

}
