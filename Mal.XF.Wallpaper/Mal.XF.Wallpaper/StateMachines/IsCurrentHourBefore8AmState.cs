using System;
using Mal.XF.Infra.Log;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui vérifie si l'heure courante est avant 8 du matin.
    /// </summary>
    internal class IsCurrentHourBefore8AmState : SwitchStateBase
    {
        private readonly ILogger logger;

        public IsCurrentHourBefore8AmState(ILogger logger)
        {
            this.logger = logger;
        }

        protected override bool IsValid()
        {
            try
            {
                var isValid = DateTime.Now.Hour < 8;
                this.logger.Debug($"Before 8Am : {isValid}");

                return isValid;
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message, e);
                throw;
            }
        }
    }
}
