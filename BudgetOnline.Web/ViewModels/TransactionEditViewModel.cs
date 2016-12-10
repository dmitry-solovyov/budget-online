using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.UI.Attributes;
using BudgetOnline.UI.Models;
using BudgetOnline.UI.Models.Editors;
using BudgetOnline.Web.UI.Validators;

namespace BudgetOnline.Web.ViewModels
{
    public class TransactionEditViewModel : IValidated
    {
        public TransactionEditViewModel()
        {
            Amount = 1m;
            SumIn = new CurrencyBundle();
            SumOut = new CurrencyBundle();
            Category = new IdWithSelectList();
            Date = DateTime.Now.Date;
            TransactionType = new IdWithSelectList { Id = (int)TransactionTypes.Expense };
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? LinkedId { get; set; }


        [UIHint("Date")]
        [DataType(DataType.Date)]
        [DisplayName("Дата")]
        [Display(Order = 1)]
        [YearRange(FromYear = 2011, ErrorMessage = "Must be after 01.01.2011")]
        public DateTime Date { get; set; }

        [DisplayName("Тип")]
        [Display(Order = 2)]
        public IdWithSelectList TransactionType { get; set; }

        [DisplayName("Категория")]
        [Display(Order = 3)]
        public IdWithSelectList Category { get; set; }


        [ScaffoldColumn(false)]
        [Range(1, double.MaxValue)]
        public decimal Amount { get; set; }


        [DisplayName("Со счета")]
        [Display(Order = 4, GroupName = "Money")]
        [ControlRowClass("hidden")]
        [ControlRowId("out_sum_group")]
        [Placeholder("Сумма или формула")]
        [GridLayout(10)]
        public CurrencyBundle SumOut { get; set; }

        [DisplayName("На счет")]
        [Display(Order = 5, GroupName = "Money")]
        [ControlRowClass("hidden")]
        [ControlRowId("in_sum_group")]
        [Placeholder("Сумма или формула")]
        public CurrencyBundle SumIn { get; set; }


        [DisplayName("Описание")]
        [UIHint("Multiline")]
        public string Description { get; set; }

        [UIHint("Autocomplete")]
        [AutocompleteOption("GetTags")]
        [DisplayName("Тэги")]
        public string Tags { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int[] TagIds { get; set; }


        [HiddenLabel]
        [DisplayName("Не учитывать")]
        public bool IsDisabled { get; set; }

        [HiddenLabel]
        [ScaffoldColumn(false)]
        [DisplayName("Продолжить добавление")]
        public bool IsCreateNewAfterSave { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedWhen { get; set; }
        [ScaffoldColumn(false)]
        public int CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? UpdatedWhen { get; set; }
        [ScaffoldColumn(false)]
        public int? UpdatedBy { get; set; }

        public IEnumerable<string> Errors()
        {
            var errors = new List<string>();

            if (!(Enum.GetValues(typeof(TransactionTypes)) as int[]).Contains(TransactionType.Id))
                errors.Add("Неправильный тип операции");

            if (TransactionType.Id == (int)TransactionTypes.Transfer)
            {
                if (SumIn.Account.Id == SumOut.Account.Id)
                    errors.Add("Счета при переводе должны быть разными");
            }

            if (TransactionType.Id == (int)TransactionTypes.Exchange)
            {
                if (SumIn.Currency.Id == SumOut.Currency.Id)
                    errors.Add("Валюты при обмене должны быть разными");
            }

            return errors;
        }
    }
}