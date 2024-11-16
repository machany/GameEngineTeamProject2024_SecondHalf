using System.Collections.Generic;
using System.Linq;
using System;

public class NotOverlapValue<T>
{
    public event Action? OnReset;

    private Random _random;

    private List<T> _range = new List<T>();
    private List<T> _values = new List<T>();

    public NotOverlapValue(IEnumerable<T> range, Random? random = null)
    {
        _range.AddRange(range);
        _values.AddRange(range);
        _random = random ?? new Random();
    }

    public NotOverlapValue(T range, Random? random = null) : this(new[] { range }, random) { }

    public NotOverlapValue(Random? random = null) : this(Enumerable.Empty<T>(), random) { }

    /// <summary>
    /// 초기화 시 기준이 될 범위의 수를 가져옵니다.
    /// </summary>
    public int GetRangeCount() => _range.Count;

    /// <summary>
    /// 초기회 되기 까지 남은 값들의 수를 가져옵니다.
    /// </summary>
    public int GetValueCount() => _values.Count;

    /// <summary>
    /// 값(<typeparamref name="T"/>)을 범위에 추가합니다.
    /// </summary>
    /// <param name="value">추가할 값입니다.</param>
    /// <remarks>
    /// 값을 추가할 수 없는 경우에 대한 예외 처리가 포함되어 있지 않습니다.
    /// </remarks>
    public void AddRange(T range)
    {
        if (range == null || _range.Contains(range)) return;
        _range.Add(range);
    }

    /// <summary>
    /// 지정된 값의 컬렉션을 범위에 추가합니다.
    /// </summary>
    /// <param name="value">추가할 값의 컬렉션입니다.</param>
    /// <remarks>
    /// null 값에 대한 예외 처리가 포함되어 있지 않습니다.
    /// </remarks>
    public void AddRange(IEnumerable<T> range)
    {
        if (range == null) return;

        foreach (T entry in range)
            if (!_range.Contains(entry))
                _range.Add(entry);
    }

    /// <summary>
    /// 값(<typeparamref name="T"/>)을 후에 나올 값에 추가합니다.
    /// 추가된 값은 초기화 되지 않고 사라집니다.
    /// </summary>
    /// <param name="value">추가할 값입니다.</param>
    /// <remarks>
    /// 값을 추가할 수 없는 경우에 대한 예외 처리가 포함되어 있지 않습니다.
    /// </remarks>
    public void AddValue(T value)
    {
        if (value == null || _range.Contains(value)) return;
        _values.Add(value);
    }

    /// <summary>
    /// 지정된 값의 컬렉션을 후에 나올 값에 추가합니다.
    /// 추가된 값은 초기화 되지 않고 사라집니다.
    /// </summary>
    /// <param name="value">추가할 값의 컬렉션입니다.</param>
    /// <remarks>
    /// null 값에 대한 예외 처리가 포함되어 있지 않습니다.
    /// </remarks>
    public void AddValue(IEnumerable<T> value)
    {
        if (value == null) return;

        foreach (T entry in value)
            if (!_values.Contains(entry))
                _values.Add(entry);
    }

    /// <summary>
    /// 범위의 값(<typeparamref name="T"/>)을 삭제합니다.
    /// </summary>
    public void RangeRemove(T value) => _range.Remove(value);

    /// <summary>
    /// n번째 값(<typeparamref name="T"/>)을 범위에서 삭제합니다.
    /// </summary>
    public void RangeRemoveAt(int n)
    {
        if (n >= 0 && n < _range.Count)
            _range.RemoveAt(n);
    }

    /// <summary>
    /// 범위를 초기화합니다.
    /// </summary>
    public void RangeClear() => _range.Clear();

    /// <summary>
    /// 범위의 값 전체을 가져옵니다.
    /// </summary>
    /// <returns>
    /// 값들(<typeparamref name="T"/>)을 반환합니다.
    /// </returns>
    public IEnumerable<T> GetRange() => _range;

    /// <summary>
    /// 값의 범위를 초기화합니다.
    /// </summary>
    public void Reset()
    {
        _values = new List<T>(_range);
        OnReset?.Invoke();
    }

    /// <summary>
    /// 렌덤으로 범위 안의 T값을 가져옵니다.
    /// 한 범위가 다 지나기 전까지 같은 값을 가져오지 않습니다.
    /// 단, 중복된 범위는 예외입니다.
    /// </summary>
    /// <returns>범위가 비어 있을 시 기본값(<typeparamref name="T"/>)을 반환합니다.</returns>
    public T GetValue()
    {
        if (_values.Count <= 0)
            Reset();

        if (_values.Count <= 0)
            return default(T);

        int randNum = _random.Next(0, _values.Count);
        T randValue = _values[randNum];
        _values.RemoveAt(randNum);
        return randValue;
    }

    /// <summary>
    /// 범위 안의 값(<typeparamref name="T"/>)을 렌덤으로 가져옵니다.
    /// </summary>
    /// <returns>값(<typeparamref name="T"/>)을 반환합니다.</returns>
    public T GetRangeInValue()
        => _range[_random.Next(0, _range.Count)];


    /// <summary>
    /// 지정된 인덱스에 해당하는 값을 반환합니다.
    /// </summary>
    /// <param name="n">가져올 값의 인덱스입니다.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// 인덱스가 범위를 벗어나는 경우 발생합니다.
    /// </exception>
    /// <remarks>
    /// 0 이상의 인덱스만 허용되며, 해당 범위 내의 인덱스여야 합니다.
    /// </remarks>
    public T GetValue(int n)
    {
        if (n < 0 || n >= _range.Count)
            throw new ArgumentOutOfRangeException("해당 매개변수에 해당하는 인덱스 값이 없습니다.");

        T randValue = _values[n];
        _values.RemoveAt(n);
        return randValue;
    }

    /// <summary>
    /// 지정된 인덱스에 해당하는 범위 안의 값을 반환합니다.
    /// </summary>
    /// <param name="n">가져올 값의 인덱스입니다.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// 인덱스가 범위를 벗어나는 경우 발생합니다.
    /// </exception>
    /// <remarks>
    /// 0 이상의 인덱스만 허용되며, 해당 범위 내의 인덱스여야 합니다.
    /// </remarks>
    public T GetRangeInValue(int n)
    {
        if (n < 0 || n >= _range.Count)
            throw new ArgumentOutOfRangeException("해당 매개변수에 해당하는 인덱스 값이 없습니다.");

        return _range[n];
    }
}