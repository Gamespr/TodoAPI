# ASP.NET Core WebApi其他實作及練習
* LINQ
  * Where()：查詢條件，符合條件的資料就會列出來，俐：Where(d=>d.length>3)
  * Select()：將資料做選擇性輸出
  * Distinct()：去除資料中重複的元素
  * Order()：將資料做升幕排序，可以指定要依哪個列做升幕排序
  * OrderByDescending()：降幕排序
  * First()：尋找符合條件的第一個元素，若無元素會跳出Exception
  * FirstOrDefault()：同上，若無元素會得到Default值
  * Single()：跟First很像會找到符合條件的第一個元素，也會在若找無元素時會跳例外，但是Single的差別是找唯一元素，所以若有第二個條件符合的元素則會跳錯
  * Count()：計算數量
  * Any()：判斷該值是否存在，有責回傳true
  * Include()：資料有設外鍵情況下使用，如果沒有關聯的話則用join()去關聯，</br> 俐：join b in _todoContext.Employee on a.InsertEmployeeId equals b.EmployeeId

