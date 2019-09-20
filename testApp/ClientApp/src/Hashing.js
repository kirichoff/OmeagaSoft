import crypto from "crypto";

export default (text)=>{
    return crypto.createHash('sha256')
        .update(text)
        .digest('base64');
}