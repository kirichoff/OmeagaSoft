const rest ={}

rest.Modify = function (user,type='update') {
    let typeStr;

    switch (type){
        case 'update': typeStr = 'UpdateUser'; break;
        case 'add': typeStr = 'AddUser'; break;
        case 'delete': typeStr = 'RemoveUser'; break;
        default: typeStr = 'UpdateUser';
    }

    let iso =  new  Date();
    let date = iso.toISOString();
    let form = new FormData()

    form.append('user',  JSON.stringify(user))
    return  fetch(`api/SampleData/${typeStr}/`,{
        method: 'POST',
        body: form
    })
        .then(data => data.json().then(d=>d))
        .catch(ex=> ex)
        ;
}
rest.getUsers = function(){
    return  fetch(`api/SampleData/GetAllUsers/`,{
        method: 'GET',
    })
        .then(data => data.json())
        .catch(ex=> ex)
        ;
}
rest.find = function (username,password) {
    return  fetch(`api/SampleData/Login/?name=${username}&password=${password}`,{
        method: 'GET',
    })
        .then(data => data.json())
        .catch(ex=> ex)
        ;
}

export default rest;