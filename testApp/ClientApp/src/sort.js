export const sort ={}

sort.byName=function (array,name) {
    let buffer = []
    for (let i = 0; i<array.length; i++){
        if (array[i].UserName === name)
            buffer.push(array[i])
    }
    return buffer
}
sort.byAction = function (array,action) {
    let buffer = []
        for (let i =0; i<array.length; i++)
            if (array[i].Action === action)
                buffer.push(array[i])
return buffer
}
sort.byActionAndName = function (array,action,name) {
    array.sort((a,b)=> {
            if ((b.Action === action) && (b.UserName === name))return 0
            if ((b.Action !== action)  && (b.UserName !==  name))return -1
            if ((b.Action === action) && (b.UserName === name))  return 1
        }
    )
}
sort.ByDate = function (array) {
    array.sort((a,b)=>{
        if(a.Date>b.Date) return-1
        if(a.Date<b.Date) return 1
        if(a.Date===b.Date) return 0
    })

    return array
}
