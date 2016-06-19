using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Core;
using RydlewskiJablonski.Quiz.DAO.BO;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Menu;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class TestViewModel : INotifyPropertyChanged
    {
        private ITest _test;
        private IDAO _dao;

        public TestViewModel(ITest test)
        {
            _dao = new DAO.DAO();
            _test = test;
            PopulateQuestions();
        }

        public TestViewModel()
        {
            _dao = new DAO.DAO();
            _test = new Test();
            _test.Questions = new List<IQuestion>();
            PopulateQuestions();
        }

        private void PopulateQuestions()
        {
            _questionViewModels = new ObservableCollection<QuestionViewModel>();
            foreach (var question in _test.Questions)
            {
                _questionViewModels.Add(new QuestionViewModel(question));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Id
        {
            get { return _test.Id; }
            set
            {
                _test.Id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _test.Name; }
            set
            {
                _test.Name = value;
                OnPropertyChanged();
            }
        }

        public bool IsMultipleChoice
        {
            get { return _test.IsMultipleChoice; }
            set
            {
                _test.IsMultipleChoice = value;
                OnPropertyChanged();
            }
        }

        public int GivenTime
        {
            get { return _test.GivenTime; }
            set
            {
                _test.GivenTime = value;
                OnPropertyChanged();
            }
        }

        public ScoringSchemas ScoringSchema
        {
            get { return _test.ScoringSchema; }
            set
            {
                _test.ScoringSchema = value;
                OnPropertyChanged();
            }
        }

        public List<IQuestion> Questions
        {
            get { return _test.Questions; }
            set
            {
                _test.Questions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<QuestionViewModel> _questionViewModels;

        public ObservableCollection<QuestionViewModel> QuestionViewModels
        {
            get { return _questionViewModels; }
            set
            {
                _questionViewModels = value;
                OnPropertyChanged();
            }
        }

        public void AddTest()
        {
            _dao.AddTest(_test);
        }
    }
}