type PersonalName = {FirstName:string; LastName:string}

// Addresses
type StreetAddress = {Line1:string; Line2:string; Line3:string }

type ZipCode =  ZipCode of string   
type StateAbbrev =  StateAbbrev of string
type ZipAndState =  {State:StateAbbrev; Zip:ZipCode }
type USAddress = {Street:StreetAddress; Region:ZipAndState}
type UKPostCode =  PostCode of string
type UKAddress = {Street:StreetAddress; Region:UKPostCode}

type InternationalAddress = {
    Street:StreetAddress; Region:string; CountryName:string}

// choice type  -- must be one of these three specific types
type Address = 
    USAddress of USAddress 
    | UKAddress of UKAddress
    | InternationalAddress of InternationalAddress

// Email
type Email = Email of string

// Phone
type CountryPrefix = Prefix of int
type Phone = {CountryPrefix:CountryPrefix; LocalNumber:string}

type Contact = 
    {
    PersonalName: PersonalName;
    // "option" means it might be missing
    Address: Address option;
    Email: Email option;
    Phone: Phone option;
    }

// Put it all together into a CustomerAccount type
type CustomerAccountId  = AccountId of string
type CustomerType  = Prospect | Active | Inactive

// override equality and deny comparison
[<CustomEquality; NoComparison>]
type CustomerAccount = 
    {
    CustomerAccountId: CustomerAccountId;
    CustomerType: CustomerType;
    ContactInfo: Contact;
    }

    override this.Equals(other) =
        match other with
        | :? CustomerAccount as otherCust -> 
          (this.CustomerAccountId = otherCust.CustomerAccountId)
        | _ -> false

    override this.GetHashCode() = hash this.CustomerAccountId

let name = {FirstName= "Bob"; LastName= "Dylan"}
let street = {Line1 = "a"; Line2 = "b"; Line3 = "c"}
let zipAndState =  {State =StateAbbrev "AB"; Zip = ZipCode "12345"}
let usAddress : USAddress =  {Street = street; Region = zipAndState}   
let c1 = 
    {
        PersonalName = name
        Address = Some (USAddress usAddress)
        Email = None
        Phone = None
    }

let c2 =
    {
        PersonalName = name
        Address = None
        Email = None
        Phone = None
    }

let newZip zip contact = 
    match contact.Address with
    | Some (USAddress us) -> { contact with Address = {us with Region = {us.Region with Zip = zip}} |> USAddress |> Some}
    | _ -> contact

let newState state contact = 
    match contact.Address with
    | Some (USAddress us) -> { contact with Address = {us with Region = {us.Region with State = state}} |> USAddress |> Some}
    | _ -> contact

newZip (ZipCode "54321") c2
newState (StateAbbrev "CD") c2