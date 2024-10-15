public class WalletPresenter
{
    private ClampedAmountWithIcon _view;
    private PlayerWallet _model;

    public WalletPresenter(ClampedAmountWithIcon view, PlayerWallet model)
    {
        _view = view;
        _model = model;
    }

    public void Enable() =>
        _model.CoinChanges += OnCoinChanges;

    public void Disable() =>
        _model.CoinChanges += OnCoinChanges;

    private void OnCoinChanges() =>
        _view.SetAmount(_model.Coin);
}