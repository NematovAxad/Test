namespace Domain
{
    public enum MonitoringProjectStatus
    {
        //Бажарилди
        Done = 1,
        //Бажарилмоқда
        InProgress,
        //Бажарилмаган
        NotDone,
        //Тасдиқлаш учун
        ForApproval,
        //Кечикиш билан ижро қилинган
        ExecutedWithDelay,
        //Бекор қилинган
        Canceled,
        //Босқичнинг якунланиши
        FinalStage

    }
}
