import rest from '../rest'

const initialState = {}

export const actionCreators = {
  Login: (userName,password) => async (dispatch, getState) => {


    const user = await rest.find(userName,password);
    dispatch({ type: "LOGIN",User: user });
  },
  Register: ({
               UserName,
               Password,
               objectValue,
               FirstName,
               LastName,
               Email}
               ) => async (dispatch, getState) =>{

      const user = {
        UserName: UserName,
        Password: Password,
        Type: objectValue === 'Admin',
        FirstName: FirstName,
        LastName: LastName,
        Email: Email,
        StartDate: new Date()
        }

       rest.Modify(user,'add')
    },
    Edit: ({
             Password,
             objectValue,
             FirstName,
             LastName,
             Email}
    ) => async (dispatch, getState) =>{

    let user = getState().User

      user.FirstName = FirstName? FirstName : user.FirstName
      user.Password = Password? Password : user.Password
      user.LastName = LastName? LastName : user.LastName
      user.Email = Email? Email : user.Email
      user.Type = Email? objectValue ==='Admin' : user.Type

      rest.Modify(user,'update')
    }


};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === "LOGIN") {
    return {
      User: action.User
    };
  }
  return state;
};
