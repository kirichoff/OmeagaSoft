import rest from '../rest'
import Hashing from "../utils/Hashing";
import authHelper from "../utils/authHelper";

const initialState = {}

export const actionCreators = {
  Login: (userName,password) => async (dispatch, getState) => {
    //const user = await rest.find(userName,Hashing(password));
    const token = await rest.getToken(userName,Hashing(password));
    authHelper.saveAuth(token.username,token.access_token);
      const user = await rest.GetUser();
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
        Password: Hashing(Password),
        Type: objectValue === 'Admin',
        FirstName: FirstName,
        LastName: LastName,
        Email: Email,
        StartDate: `${new Date().toISOString()}`
        }

       let res = await rest.Modify(user,'add')
      dispatch({type:'REGISTER_TRUE',res: res })
    },
    Edit: ({
             Password,
             objectValue,
             FirstName,
             LastName,
             Email}
    ) => async (dispatch, getState) =>{
    let user = getState().User.User
      user.FirstName = FirstName? FirstName : user.FirstName
      user.Password = Password? Hashing(Password) : user.Password
      user.LastName = LastName? LastName : user.LastName
      user.Email = Email? Email : user.Email
      user.Type = objectValue? objectValue ==='Admin' : user.Type
        console.log('USER',user)
      let res = await rest.Modify(user,'update')
        if (res){
             user =  await rest.GetUser();
             console.log('USER@',user)
            dispatch({type: 'LOGIN',User: user})
        }

        return res
    },
    getJournal: ()=> async (dispatch,getState)=>{
        const journal = await rest.getJournal();
        dispatch({type: 'JOURNAL',journal: journal })
        }

};

export const reducer = (state, action) => {
  state = state || initialState;

  switch (action.type) {
      case "LOGIN":
          return {
          ...state,
          User: action.User
      };
      case 'REGISTER_TRUE':  return {
          ...state,
          isRegister: action.res
      }
      case 'JOURNAL' : return {
          ...state,
          Journal: action.journal
      }
  }
  return state;
};
