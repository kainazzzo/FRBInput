﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gum.DataTypes;
using Gum.Managers;



namespace Gum.Managers
{
    public enum PositionUnitType
    {
        PixelsFromLeft,
        PixelsFromTop,
        PercentageWidth,
        PercentageHeight,
        PixelsFromRight,
        PixelsFromBottom,
        PixelsFromCenterX,
        PixelsFromCenterY,
        PixelsFromCenterYInverted


    }
}


namespace Gum.Converters
{

    public enum GeneralUnitType
    {
        PixelsFromSmall,
        PixelsFromLarge,
        PixelsFromMiddle,
        Percentage,
        PercentageOfFile,
        PixelsFromMiddleInverted
    }

    public static class GeneralUnitTypeExtensions
    {
        public static bool GetIsPixelBased(this GeneralUnitType unitType)
        {
            return unitType == GeneralUnitType.PixelsFromSmall ||
                unitType == GeneralUnitType.PixelsFromMiddle ||
                unitType == GeneralUnitType.PixelsFromMiddleInverted ||
                unitType == GeneralUnitType.PixelsFromLarge;
        }

    }


    public enum XOrY
    {
        X,
        Y
    }

    public class UnitConverter
    {
        static UnitConverter mSelf = new UnitConverter();


        public static UnitConverter Self
        {
            get { return mSelf; }
        }


        public void ConvertToPixelCoordinates(float relativeX, float relativeY, GeneralUnitType generalX, GeneralUnitType generalY, float parentWidth, float parentHeight, 
            float fileWidth, float fileHeight, out float absoluteX, out float absoluteY)
        {
            absoluteX = relativeX;
            absoluteY = relativeY;

            if (generalX == GeneralUnitType.Percentage)
            {
                absoluteX = parentWidth * relativeX / 100.0f;
            }
            else if (generalX == GeneralUnitType.PixelsFromMiddle)
            {
                absoluteX = parentWidth / 2.0f + relativeX;
            }
            else if (generalX == GeneralUnitType.PixelsFromLarge)
            {
                absoluteX = parentWidth + relativeX;
            }
            else if (generalX == GeneralUnitType.PercentageOfFile)
            {
                absoluteX = fileWidth * relativeX / 100.0f;
            }


            if (generalY == GeneralUnitType.Percentage)
            {
                absoluteY = parentHeight * relativeY / 100.0f;
            }
            else if (generalY == GeneralUnitType.PixelsFromMiddle)
            {
                absoluteY = parentHeight / 2.0f + relativeY;
            }
            else if(generalY == GeneralUnitType.PixelsFromMiddleInverted)
            {
                absoluteY = parentHeight / 2.0f - relativeY;
            }
            else if (generalY == GeneralUnitType.PixelsFromLarge)
            {
                absoluteY = parentHeight + relativeY;
            }
            else if (generalY == GeneralUnitType.PercentageOfFile)
            {
                absoluteY = fileHeight * relativeY / 100.0f;
            }
        }


        public void ConvertToUnitTypeCoordinates(float absoluteX, float absoluteY, GeneralUnitType generalX, GeneralUnitType generalY, float parentWidth, float parentHeight, float fileWidth, float fileHeight, out float relativeX, out float relativeY)
        {
            relativeX = absoluteX;
            relativeY = absoluteY;

            if (generalX == GeneralUnitType.Percentage)
            {
                relativeX = 100 * absoluteX / parentWidth;
            }
            else if (generalX == GeneralUnitType.PixelsFromMiddle)
            {
                relativeX = absoluteX - parentWidth / 2.0f;
            }
            else if (generalX == GeneralUnitType.PixelsFromLarge)
            {
                relativeX = absoluteX - parentWidth;
            }
            else if (generalX == GeneralUnitType.PercentageOfFile)
            {
                relativeX = 100 * absoluteX / fileWidth;
            }

            if (generalY == GeneralUnitType.Percentage)
            {
                relativeY = 100 * absoluteY / parentHeight;
            }
            else if (generalY == GeneralUnitType.PixelsFromMiddle)
            {
                relativeY = absoluteY - parentHeight / 2.0f;
            }
            else if(generalY == GeneralUnitType.PixelsFromMiddleInverted)
            {
                relativeY = -absoluteY - parentHeight / 2.0f;
            }
            else if (generalY == GeneralUnitType.PixelsFromLarge)
            {
                relativeY = absoluteY - parentHeight;
            }
            else if (generalY == GeneralUnitType.PercentageOfFile)
            {
                relativeY = 100 * absoluteY / fileHeight;
            }

        }

        public static bool TryConvertToGeneralUnit(object unitType, out GeneralUnitType result)
        {
            result = GeneralUnitType.PixelsFromSmall;
            try
            {
                result = ConvertToGeneralUnit(unitType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static GeneralUnitType ConvertToGeneralUnit(object unitType)
        {
            if(unitType == null)
            {
                return GeneralUnitType.PixelsFromSmall;
            }
            else if(unitType is PositionUnitType)
            {
                return ConvertToGeneralUnit((PositionUnitType) unitType);
            }
            else if(unitType is DimensionUnitType)
            {
                return ConvertToGeneralUnit((DimensionUnitType)unitType);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static GeneralUnitType ConvertToGeneralUnit(PositionUnitType unitType)
        {
            switch (unitType)
            {
                case PositionUnitType.PercentageHeight:
                case PositionUnitType.PercentageWidth:
                    return GeneralUnitType.Percentage;
                case PositionUnitType.PixelsFromLeft:
                case PositionUnitType.PixelsFromTop:
                    return GeneralUnitType.PixelsFromSmall;
                case PositionUnitType.PixelsFromRight:
                case PositionUnitType.PixelsFromBottom:
                    return GeneralUnitType.PixelsFromLarge;
                case PositionUnitType.PixelsFromCenterX:
                    return GeneralUnitType.PixelsFromMiddle;
                case PositionUnitType.PixelsFromCenterY:
                    return GeneralUnitType.PixelsFromMiddle;
                case PositionUnitType.PixelsFromCenterYInverted:
                    return GeneralUnitType.PixelsFromMiddleInverted;
                default:
                    throw new NotImplementedException();
            }
        }

        public static GeneralUnitType ConvertToGeneralUnit(DimensionUnitType unitType)
        { 
            switch (unitType)
            {
                case DimensionUnitType.Absolute:
                    return GeneralUnitType.PixelsFromSmall;
                case DimensionUnitType.Percentage:
                    return GeneralUnitType.Percentage;
                case DimensionUnitType.RelativeToContainer:
                    return GeneralUnitType.PixelsFromLarge;
                case DimensionUnitType.PercentageOfSourceFile:
                    return GeneralUnitType.PercentageOfFile;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
