import CryptoJS from "crypto-js";

export default class MyEncript {
  public encrypt(valueToEncript: string): string {
    const keySize = 256;
    const salt = CryptoJS.lib.WordArray.random(16);
    const key = CryptoJS.PBKDF2(process.env.VUE_APP_KEYSECRET, salt, {
      keySize: keySize / 32,
      iterations: 100
    });

    const iv = CryptoJS.lib.WordArray.random(128 / 8);

    const encrypted = CryptoJS.AES.encrypt(valueToEncript, key, {
      iv: iv,
      padding: CryptoJS.pad.Pkcs7,
      mode: CryptoJS.mode.CBC
    });

    const result = CryptoJS.enc.Base64.stringify(salt.concat(iv).concat(encrypted.ciphertext));

    return result;
  }

  public decrypt(valueToDecrypt: string): string {
    const key = CryptoJS.enc.Utf8.parse(process.env.VUE_APP_KEYSECRET);
    const iv = CryptoJS.lib.WordArray.create([0x00, 0x00, 0x00, 0x00]);

    const decrypted = CryptoJS.AES.decrypt(valueToDecrypt, key, { iv: iv });
    return decrypted.toString(CryptoJS.enc.Utf8);
  }
}
