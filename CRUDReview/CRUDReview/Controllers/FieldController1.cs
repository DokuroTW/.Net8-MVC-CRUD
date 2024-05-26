using CRUDReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDReview.Controllers
{
    public class FieldController1 : Controller
    {
        private readonly FieldContext fieldContext;  // 引用SQL SERVER 且fieldContext字段不能被修改

        public FieldController1(FieldContext fieldContext)   //依賴注入使fieldContext可以被使用
        {
            this.fieldContext = fieldContext;       //資料庫物件
        } 
        //Create
        [HttpGet]
        public IActionResult Add()
        {
            return View(); //呼叫  FieldController1/Add.cshtml
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddFieldViewModel viewModel)
        //                    非同步                                        宣告                      物件
        {
            Field field = new Field                        
            {
                Name = viewModel.Name,
                Title = viewModel.Title,
                Content = viewModel.Content
            };
            await fieldContext.Fields.AddAsync(field); //待新增
            await fieldContext.SaveChangesAsync();   //保存

            return RedirectToAction("List", "FieldController1");  //("function","controller")
        }

        //List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var fields = await fieldContext.Fields.ToListAsync();  //將Table資料全撈出

            return View(fields); // 呼叫  FieldController1/List.cshtml 並傳值
        }

        //Read
        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var field = await fieldContext.Fields.FindAsync(id);  //將與id 相同的 record 撈出
            if (field is not null)
            {
                return View(field); //呼叫  FieldController1/Detail.cshtml 並傳值
            }

            return NotFound(); //回傳 404 Not Found
        }

        //Update
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var field = await fieldContext.Fields.FindAsync(id); //將與id 相同的 record 撈出

            return View(field); //呼叫  FieldController1/edit.cshtml 並傳值
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Field viewModel)
        {
            var field = await fieldContext.Fields.FindAsync(viewModel.Id);  //將與viewModel中的id 相同的 record 撈出
            if (field is not null)
            {
                field.Name = viewModel.Name;
                field.Title = viewModel.Title;
                field.Content = viewModel.Content;    

                await fieldContext.SaveChangesAsync();
            }                                                                                               // 重新附值並保存
            return RedirectToAction("List", "FieldController1"); //("function","controller")
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {                                                                                                                           //lambda 方法 將fieldContext的Table Fields中 撈出與id相同的record
            var field = await fieldContext.Fields.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);   
            //                                                   提升撈資料的效能    取得第一個如果沒有則為null
            if (field is not null)
            {
                fieldContext.Fields.Remove(field);     //待刪除
                await fieldContext.SaveChangesAsync();  //保存
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("List", "FieldController1"); //("function","controller")            return RedirectToAction("List", "FieldController1"); //("function","controller")
        }
    }
}
