export default class CommonDataManager {
  static myInstance = null;

  musteriNo = '';

  /**
   * @returns {CommonDataManager}
   */
  static getInstance() {
    if (CommonDataManager.myInstance == null) {
      CommonDataManager.myInstance = new CommonDataManager();
    }

    return this.myInstance;
  }

  getMusteriNo() {
    return this.musteriNo;
  }

  setMusteriNo(id) {
    this.musteriNo = id;
  }
}
